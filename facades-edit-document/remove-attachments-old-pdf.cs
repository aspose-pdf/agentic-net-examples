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

        try
        {
            // Bind the PDF using the Facade API
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Use PdfExtractor to obtain the list of attachment names
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPath);
            extractor.ExtractAttachment();                     // Must be called before GetAttachNames()
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Delete attachments whose names end with "_old.pdf"
            foreach (string name in attachmentNames)
            {
                if (name.EndsWith("_old.pdf", StringComparison.OrdinalIgnoreCase))
                {
                    // EmbeddedFiles collection is accessible via the underlying Document
                    editor.Document.EmbeddedFiles.Delete(name);
                }
            }

            // Save the modified PDF
            editor.Save(outputPath);
            Console.WriteLine($"Attachments removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}