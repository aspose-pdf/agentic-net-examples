using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string mainPdfPath = "main.pdf";                 // PDF to receive the attachment
        const string attachmentPdfPath = "attachment.pdf";     // Original attachment (must be PDF)
        const string encryptedAttachment = "attachment_encrypted.pdf"; // Temp encrypted file
        const string outputPdfPath = "main_with_attachment.pdf";

        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify input files exist
        if (!File.Exists(mainPdfPath))
        {
            Console.Error.WriteLine($"Main PDF not found: {mainPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPdfPath))
        {
            Console.Error.WriteLine($"Attachment PDF not found: {attachmentPdfPath}");
            return;
        }

        try
        {
            // ---------- Encrypt the attachment ----------
            using (Document attachDoc = new Document(attachmentPdfPath))
            {
                // Allow printing and content extraction; adjust as needed
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt with AES‑256 (strongest supported algorithm)
                attachDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted version to a temporary file
                attachDoc.Save(encryptedAttachment);
            }

            // ---------- Add the encrypted attachment to the main PDF ----------
            using (Document mainDoc = new Document(mainPdfPath))
            {
                // Load the encrypted file into a memory stream
                byte[] encryptedBytes = File.ReadAllBytes(encryptedAttachment);
                var fileSpec = new FileSpecification(Path.GetFileName(encryptedAttachment), "Encrypted attachment");
                fileSpec.Contents = new MemoryStream(encryptedBytes);
                fileSpec.MIMEType = "application/pdf"; // optional but helpful

                // Add the file specification to the EmbeddedFiles collection
                mainDoc.EmbeddedFiles.Add(fileSpec);

                // Save to the output path (leaving the original untouched)
                mainDoc.Save(outputPdfPath);
            }

            // Optional: clean up the temporary encrypted file
            if (File.Exists(encryptedAttachment))
            {
                File.Delete(encryptedAttachment);
            }

            Console.WriteLine($"Attachment encrypted and added successfully. Output: {outputPdfPath}");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
