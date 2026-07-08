using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use PdfExtractor to obtain the list of attachment names
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPath);
                extractor.ExtractAttachment();                     // required before GetAttachNames
                IList<string> attachmentNames = extractor.GetAttachNames();

                // Delete attachments whose names end with "_old.pdf"
                foreach (string name in attachmentNames)
                {
                    if (name.EndsWith("_old.pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        doc.EmbeddedFiles.Delete(name);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}