using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "secured.pdf";          // Encrypted PDF file
        const string userPassword  = "userpass";             // Password to open the PDF
        const string attachment    = "attachment.txt";       // File to attach
        const string description   = "Sample attachment";    // Description for the attachment
        const string outputPdf     = "secured_with_attachment.pdf";

        // Verify that required files exist
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

        // Load the encrypted PDF using the password
        using (Document doc = new Document(inputPdf, userPassword))
        {
            // Initialize the content editor with the loaded document
            PdfContentEditor editor = new PdfContentEditor(doc);

            // Add the file attachment (no visual annotation)
            editor.AddDocumentAttachment(attachment, description);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}