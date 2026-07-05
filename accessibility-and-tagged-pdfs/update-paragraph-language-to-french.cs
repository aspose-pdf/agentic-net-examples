using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;                 // ITaggedContent
using Aspose.Pdf.LogicalStructure;      // StructureElement, ParagraphElement, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_french.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates it if missing)
            ITaggedContent tagged = doc.TaggedContent;

            // Locate the target ParagraphElement (replace identification logic as needed)
            ParagraphElement target = null;
            var paragraphs = tagged.RootElement.FindElements<ParagraphElement>(true);
            foreach (var para in paragraphs)
            {
                if (!string.IsNullOrEmpty(para.ActualText) && para.ActualText.Contains("Target Text"))
                {
                    target = para;
                    break;
                }
            }

            if (target == null)
            {
                Console.WriteLine("Target structure element not found.");
            }
            else
            {
                // Update the language to French (RFC 3066 tag)
                target.Language = "fr-FR";
                Console.WriteLine("Language updated to French for the selected element.");
            }

            // OPTIONAL: Convert the document to PDF/UA compliance if required.
            // This uses Document.Convert instead of the Facades‑based PdfValidator.
            // Uncomment and adjust the parameters if compliance conversion is desired.
            //
            // try
            // {
            //     doc.Convert("validation_log.xml", PdfFormat.PDF_UA_1, ConvertErrorAction.Delete);
            //     Console.WriteLine("Document converted to PDF/UA compliance.");
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"Compliance conversion failed: {ex.Message}");
            // }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
