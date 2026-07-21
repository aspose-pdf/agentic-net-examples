using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Define the new attachments (file path and description)
        var attachments = new (string Path, string Description)[]
        {
            ("file1.txt", "First attachment"),
            ("image.png", "Image attachment")
        };

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Verify each attachment file exists
        foreach (var att in attachments)
        {
            if (!File.Exists(att.Path))
            {
                Console.Error.WriteLine($"Attachment not found: {att.Path}");
                return;
            }
        }

        // Create a PdfContentEditor facade to edit attachments
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF document
        editor.BindPdf(inputPdf);

        // Remove all existing attachments
        editor.DeleteAttachments();

        // Add the fresh set of attachments
        foreach (var att in attachments)
        {
            editor.AddDocumentAttachment(att.Path, att.Description);
        }

        // Save the modified PDF to a new file
        editor.Save(outputPdf);

        // Close the facade (not IDisposable, but has Close method)
        editor.Close();

        Console.WriteLine($"Attachments refreshed and saved to '{outputPdf}'.");
    }
}