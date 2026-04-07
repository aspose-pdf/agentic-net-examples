using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string attachmentFilePath = "attachment.pdf";
        const string tempPdfPath = "temp_with_attachment.pdf";
        const string encryptedPdfPath = "encrypted_output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

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

        // Step 1: Add attachment with a custom description using PdfContentEditor
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);
        editor.AddDocumentAttachment(attachmentFilePath, "Custom description for the attachment");
        editor.Save(tempPdfPath);

        // Step 2: Open the PDF that now contains the attachment and encrypt it with AES‑256
        using (Document doc = new Document(tempPdfPath))
        {
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPdfPath);
        }

        // Optional cleanup of the intermediate file
        try
        {
            File.Delete(tempPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"Encrypted PDF with attachment saved to '{encryptedPdfPath}'.");
    }
}