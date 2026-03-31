using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document document = new Document(inputPath))
            {
                // Use permission flags that exist in all supported Aspose.Pdf versions.
                // "ModifyAnnotations" is not available in older releases, so we combine
                // commonly supported flags instead.
                Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the PDF with AES‑256.
                document.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF locally first.
                document.Save(outputPath);
            }

            // TODO: Upload the encrypted file to a cloud storage bucket (e.g., AWS S3, Azure Blob, GCS).
            // Example (pseudo‑code):
            // CloudStorage.UploadFile(bucketName, outputPath);

            Console.WriteLine($"PDF encrypted and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
