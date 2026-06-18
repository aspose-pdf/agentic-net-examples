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

        // Use PdfContentEditor (Facade) to bind the PDF and later save it.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Use PdfExtractor to obtain the list of attachment names.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            extractor.ExtractAttachment();                     // Must be called before GetAttachNames()
            IList<string> attachmentNames = extractor.GetAttachNames();

            foreach (string name in attachmentNames)
            {
                // Remove attachments whose file name ends with "_old.pdf"
                if (name != null && name.EndsWith("_old.pdf", StringComparison.OrdinalIgnoreCase))
                {
                    // The underlying Document is accessible via the editor.
                    // EmbeddedFiles collection provides Delete(string) to remove by name.
                    editor.Document.EmbeddedFiles.Delete(name);
                    Console.WriteLine($"Deleted attachment: {name}");
                }
            }
        }

        // Save the modified PDF.
        editor.Save(outputPath);
        // Close the editor (optional but good practice).
        editor.Close();

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}