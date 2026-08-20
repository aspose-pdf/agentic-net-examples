using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "secured.pdf";
        const string outputPdfPath = "secured_with_attachment.pdf";
        const string userPassword = "userpass";
        const string attachmentFilePath = "attachment.txt";
        const string attachmentDescription = "Sample attachment";

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

        // Open the encrypted PDF using the password
        using (Document doc = new Document(inputPdfPath, userPassword))
        {
            // Initialize the content editor with the opened document
            PdfContentEditor editor = new PdfContentEditor(doc);

            // Add the file attachment (no annotation)
            editor.AddDocumentAttachment(attachmentFilePath, attachmentDescription);

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}