using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, ParagraphElement

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string targetTitle = "Original Paragraph Title";
        const string correctedActualText = "Corrected Actual Text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (must be a tagged PDF)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Find all paragraph elements recursively
            var paragraphs = root.FindElements<ParagraphElement>(true);

            // Locate the paragraph whose displayed text matches the target title
            foreach (ParagraphElement para in paragraphs)
            {
                // The visible text of the paragraph is stored in ActualText
                if (para.ActualText == targetTitle)
                {
                    // Modify the ActualText attribute (used for accessibility / correction)
                    para.ActualText = correctedActualText;
                    Console.WriteLine("Paragraph ActualText updated.");
                    break; // assuming only one match is needed
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}