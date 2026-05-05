using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "secured.pdf";               // Encrypted PDF
        const string outputPdf  = "secured_with_attachment.pdf";
        const string password   = "userpass";                  // User/owner password
        const string attachFile = "attachment.txt";            // File to attach
        const string description = "Sample attachment";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachFile}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password
            using (Document doc = new Document(inputPdf, password))
            {
                // Initialize the Facade editor with the opened document
                using (PdfContentEditor editor = new PdfContentEditor(doc))
                {
                    // Add the file attachment (no visual annotation)
                    editor.AddDocumentAttachment(attachFile, description);

                    // Save the modified PDF
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