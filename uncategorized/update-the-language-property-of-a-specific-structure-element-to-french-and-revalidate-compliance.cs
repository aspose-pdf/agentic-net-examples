using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_french.pdf";
        const string logPath = "validation_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Set the document's default language (optional but recommended)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("fr-FR");

            // Locate a specific structure element (first paragraph) and set its language to French
            StructureElement root = tagged.RootElement;
            var paragraph = root.FindElements<ParagraphElement>(true).FirstOrDefault();
            if (paragraph != null)
            {
                paragraph.Language = "fr-FR";
                Console.WriteLine("Paragraph element language updated to French.");
            }
            else
            {
                Console.WriteLine("No paragraph element found to update.");
            }

            // Re‑validate compliance by converting to PDF/A‑1B (validation performed during conversion)
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}