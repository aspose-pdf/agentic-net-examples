using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (encrypted), its password, attachment file and output PDF paths
        const string inputPdfPath   = "secured.pdf";
        const string userPassword   = "userPass";
        const string attachmentPath = "attachment.txt";
        const string outputPdfPath  = "output.pdf";

        // Validate files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Open the encrypted PDF using its password
        using (Document doc = new Document(inputPdfPath, userPassword))
        {
            // Initialize the Facade editor with the opened document
            PdfContentEditor editor = new PdfContentEditor(doc);

            // Add the file attachment (no visual annotation)
            editor.AddDocumentAttachment(attachmentPath, "Sample attachment description");

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}