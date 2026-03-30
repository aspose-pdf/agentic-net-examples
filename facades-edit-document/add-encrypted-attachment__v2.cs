using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.pdf";
        const string description = "Custom attachment description";
        const string intermediatePdf = "temp.pdf";
        const string encryptedPdf = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Add the attachment (no visual annotation)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        editor.AddDocumentAttachment(attachmentFile, description);
        editor.Save(intermediatePdf);
        // PdfContentEditor does not implement IDisposable; explicit close is optional
        // editor.Close();

        // Encrypt the resulting PDF using AES‑256
        using (Document doc = new Document(intermediatePdf))
        {
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPdf);
        }

        Console.WriteLine($"Encrypted PDF with attachment saved as '{encryptedPdf}'.");
    }
}