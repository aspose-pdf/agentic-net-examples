using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "secret.txt";
        const string description = "Confidential attachment encrypted with AES";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        const string outputPdf = "output_encrypted.pdf";

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

        // Add the attachment (no annotation) with a custom description
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);
            editor.AddDocumentAttachment(attachmentFile, description);
            editor.Save(outputPdf);
        }

        // Encrypt the PDF using AES-256
        using (Document doc = new Document(outputPdf))
        {
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added and PDF encrypted: {outputPdf}");
    }
}