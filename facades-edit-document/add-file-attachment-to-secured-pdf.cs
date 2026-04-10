using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "secured.pdf";          // Encrypted PDF file
        const string userPassword = "userpwd";              // Password to open the PDF
        const string attachmentFilePath = "attachment.txt"; // File to attach
        const string attachmentDescription = "Sample attachment";
        const string outputPdfPath = "secured_with_attachment.pdf";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Open the encrypted PDF using the provided password
        using (Document doc = new Document(inputPdfPath, userPassword))
        {
            // Initialize the content editor with the opened document
            PdfContentEditor editor = new PdfContentEditor(doc);

            // Add the file attachment (no visual annotation)
            editor.AddDocumentAttachment(attachmentFilePath, attachmentDescription);

            // Save the modified PDF to a new file
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}