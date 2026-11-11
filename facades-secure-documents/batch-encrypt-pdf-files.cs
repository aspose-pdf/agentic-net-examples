using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing PDFs to encrypt
        const string sourceDirectory = @"C:\PdfFiles";
        // Output directory for encrypted PDFs
        const string outputDirectory = @"C:\PdfFiles\Encrypted";

        // Passwords used for all files
        const string userPassword  = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        // Ensure output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Permissions to grant after encryption (example: allow printing and content extraction)
        Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

        // Iterate over all PDF files in the source directory
        foreach (string inputPath in Directory.GetFiles(sourceDirectory, "*.pdf"))
        {
            try
            {
                // Determine output file name (same name with "_encrypted" suffix)
                string fileName   = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDirectory, $"{fileName}_encrypted.pdf");

                // Load the source PDF into a Document object
                Document doc = new Document(inputPath);

                // Apply AES‑256 encryption with the specified passwords and permissions
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPath);

                Console.WriteLine($"Encrypted: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }
}
