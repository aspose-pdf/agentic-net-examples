using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string targetTitle   = "Original Title";   // title to locate
        const string correctedText = "Corrected Title"; // new ActualText

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // Recursively find all paragraph elements
            var paragraphs = root.FindElements<ParagraphElement>(true);

            foreach (ParagraphElement para in paragraphs)
            {
                // Compare the existing ActualText with the title we are looking for
                if (para.ActualText == targetTitle)
                {
                    // Modify the ActualText to the corrected value
                    para.ActualText = correctedText;
                    Console.WriteLine("Paragraph ActualText updated.");
                    break; // stop after first match
                }
            }

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Corrected PDF saved to '{outputPath}'.");
    }
}