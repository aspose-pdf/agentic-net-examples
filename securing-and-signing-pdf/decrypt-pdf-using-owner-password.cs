using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the encrypted PDF file.
        const string inputPath = "encrypted.pdf";

        // Path where the decrypted PDF will be saved.
        const string outputPath = "decrypted.pdf";

        // Owner password that grants full access to the document.
        const string ownerPassword = "owner123";

        // Verify that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted document using the owner password.
            // The constructor (string filename, string password) accepts either user or owner password.
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Decrypt the document. After this call the PDF is no longer encrypted.
                doc.Decrypt();

                // Save the decrypted version. Overwrite the original or write to a new file.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Decryption successful. Decrypted file saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            // Thrown when the supplied password is incorrect or the document is not encrypted.
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other errors (e.g., I/O issues).
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}