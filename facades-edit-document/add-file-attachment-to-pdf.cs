using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // PDF to which the attachment will be added
        const string attachment = "Terms.pdf";      // File to attach
        const string outputPdf  = "output.pdf";     // Resulting PDF

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

        try
        {
            // PdfContentEditor does NOT implement IDisposable, so do NOT wrap it in a using block
            PdfContentEditor editor = new PdfContentEditor();

            // Load the existing PDF
            editor.BindPdf(inputPdf);

            // Add the file attachment with the required description
            editor.AddDocumentAttachment(attachment, "Contract Terms");

            // Save the modified PDF
            editor.Save(outputPdf);

            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}