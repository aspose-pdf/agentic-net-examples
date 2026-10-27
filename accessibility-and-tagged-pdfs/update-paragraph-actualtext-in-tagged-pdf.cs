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
        const string targetTitle = "Original Paragraph Title"; // Title to locate
        const string correctedActualText = "Corrected paragraph text.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF is tagged by checking TaggedContent is not null
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. Tagging is required to modify structure elements.");
                return;
            }

            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Retrieve all paragraph elements recursively
            var paragraphs = root.FindElements<ParagraphElement>(true);

            // Locate the paragraph whose title matches the target (using AlternativeText or ActualText)
            ParagraphElement targetParagraph = null;
            foreach (var para in paragraphs)
            {
                if (!string.IsNullOrEmpty(para.AlternativeText) &&
                    para.AlternativeText.Equals(targetTitle, StringComparison.OrdinalIgnoreCase))
                {
                    targetParagraph = para;
                    break;
                }

                if (!string.IsNullOrEmpty(para.ActualText) &&
                    para.ActualText.Equals(targetTitle, StringComparison.OrdinalIgnoreCase))
                {
                    targetParagraph = para;
                    break;
                }
            }

            if (targetParagraph == null)
            {
                Console.WriteLine($"Paragraph with title \"{targetTitle}\" not found.");
                return;
            }

            // Update the ActualText property with the corrected content
            targetParagraph.ActualText = correctedActualText;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
