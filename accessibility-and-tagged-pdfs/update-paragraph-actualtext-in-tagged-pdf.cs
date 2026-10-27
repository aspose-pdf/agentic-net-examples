using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string outputPath  = "output.pdf";
        const string targetTitle = "Original Title";   // Title to locate
        const string corrected   = "Corrected Title"; // New ActualText

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the PDF is tagged by checking TaggedContent (IsTagged does not exist)
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No structure to modify.");
                return;
            }

            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the structure tree (no cast required)
            StructureElement root = tagged.RootElement;

            // Recursively find all paragraph elements
            var paragraphs = root.FindElements<ParagraphElement>(true);

            bool updated = false;
            foreach (ParagraphElement para in paragraphs)
            {
                // Locate the paragraph by its current ActualText (acting as title)
                if (string.Equals(para.ActualText, targetTitle, StringComparison.OrdinalIgnoreCase))
                {
                    // Modify the ActualText property with the corrected value
                    para.ActualText = corrected;
                    updated = true;
                    break;
                }
            }

            if (!updated)
            {
                Console.WriteLine($"Paragraph with title '{targetTitle}' not found.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Paragraph corrected and saved to '{outputPath}'.");
        }
    }
}
