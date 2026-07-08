using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "secured_input.pdf";   // Encrypted PDF file
        const string outputPdf  = "output_with_attachment.pdf";
        const string attachment = "attachment_file.pdf"; // File to attach
        const string password   = "userPassword";       // User (or owner) password

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
            // Open the encrypted PDF using the password.
            using (Document doc = new Document(inputPdf, password))
            {
                // Initialize the content editor with the opened document.
                using (PdfContentEditor editor = new PdfContentEditor(doc))
                {
                    // Add the file attachment (no visual annotation is created).
                    editor.AddDocumentAttachment(attachment, "Attached file description");

                    // Save the modified PDF.
                    editor.Save(outputPdf);
                }
            }

            Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}