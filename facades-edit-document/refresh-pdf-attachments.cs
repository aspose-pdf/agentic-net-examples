using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that may contain attachments
        const string inputPdf  = "input.pdf";
        // Output PDF after clearing and adding new attachments
        const string outputPdf = "output.pdf";

        // Files to be attached to the PDF (path and description)
        var newAttachments = new (string Path, string Description)[]
        {
            ("file1.docx", "First document attachment"),
            ("image1.png",  "Sample image attachment"),
            ("data.csv",    "CSV data attachment")
        };

        // Validate input PDF existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Validate each attachment file existence before processing
        foreach (var (path, _) in newAttachments)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Attachment file not found: {path}");
                return;
            }
        }

        // Use PdfContentEditor (a facade) to manipulate attachments
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Remove all existing attachments
            editor.DeleteAttachments();

            // Add the fresh set of attachments
            foreach (var (path, description) in newAttachments)
            {
                editor.AddDocumentAttachment(path, description);
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachments refreshed. Output saved to '{outputPdf}'.");
    }
}