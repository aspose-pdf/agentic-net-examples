using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // PDF to which the attachment will be added
        const string attachment = "attachment_file.pdf"; // File to attach
        const string description = "Sample attachment with custom MIME type";
        const string outputPdf  = "output_with_attachment.pdf";

        // Verify that source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        // Create the facade, bind the existing PDF, add the attachment, and save the result.
        // PdfContentEditor does not expose a MimeType property on FileSpecification.
        // The MIME type cannot be set directly; only the description can be supplied.
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            editor.BindPdf(inputPdf);

            // Add the attachment with a description.
            // The overload used does not allow specifying a MIME type.
            editor.AddDocumentAttachment(attachment, description);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }
        finally
        {
            // Ensure resources are released.
            editor.Close();
        }

        Console.WriteLine($"Attachment added. Output saved to '{outputPdf}'.");
    }
}