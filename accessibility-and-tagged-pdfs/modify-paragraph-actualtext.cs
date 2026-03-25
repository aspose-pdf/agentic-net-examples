using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string targetTitle = "Original Title"; // title to locate
        const string correctedActualText = "Corrected Title"; // new ActualText value

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the PDF has a tagged structure
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged; no structure to modify.");
                doc.Save(outputPath);
                return;
            }

            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Find all paragraph elements recursively
            var paragraphs = root.FindElements<ParagraphElement>(true);
            bool updated = false;
            foreach (ParagraphElement para in paragraphs)
            {
                // Compare the displayed text (ActualText) with the target title
                if (!string.IsNullOrEmpty(para.ActualText) &&
                    para.ActualText.Equals(targetTitle, StringComparison.OrdinalIgnoreCase))
                {
                    // Update the ActualText property
                    para.ActualText = correctedActualText;
                    updated = true;
                    break;
                }
            }

            if (!updated)
                Console.WriteLine($"Paragraph with title '{targetTitle}' not found.");

            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
