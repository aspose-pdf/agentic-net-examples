using System;
using System.IO;
using System.Collections.Generic;
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

        // Load the PDF document using a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use PdfExtractor (Facade) to obtain the list of attachment names
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPath);
            extractor.ExtractAttachment(); // Must be called before GetAttachNames()
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Delete attachments whose names end with "_old.pdf"
            foreach (string name in attachmentNames)
            {
                if (name != null && name.EndsWith("_old.pdf", StringComparison.OrdinalIgnoreCase))
                {
                    // EmbeddedFileCollection provides Delete(string) to remove by name
                    doc.EmbeddedFiles.Delete(name);
                    Console.WriteLine($"Deleted attachment: {name}");
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}