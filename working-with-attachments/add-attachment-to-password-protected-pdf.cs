using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "protected.pdf";
        const string outputPath = "protected_with_attachment.pdf";
        const string attachmentPath = "attachment.txt";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Create a FileSpecification for the attachment
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath));
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));

                // Add the file to the EmbeddedFiles collection (the correct API for attachments)
                doc.EmbeddedFiles.Add(fileSpec);

                // Re‑encrypt the document (preserve original passwords and permissions)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Attachment added and saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
