using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Temporary file to hold the encrypted PDF
        string encryptedTempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPath);

            // Define permissions and encrypt the document using AES‑256
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF to a temporary file
            doc.Save(encryptedTempPath);

            // Load the encrypted PDF into a memory stream for network transmission
            using (MemoryStream pdfStream = new MemoryStream())
            {
                using (FileStream fileStream = new FileStream(encryptedTempPath, FileMode.Open, FileAccess.Read))
                {
                    fileStream.CopyTo(pdfStream);
                }
                pdfStream.Position = 0; // reset stream position

                // At this point 'pdfStream' contains the encrypted PDF bytes and can be sent over the network
                Console.WriteLine($"Encrypted PDF prepared in memory stream (Length: {pdfStream.Length} bytes).");
            }
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary encrypted file
            if (File.Exists(encryptedTempPath))
            {
                try { File.Delete(encryptedTempPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}
