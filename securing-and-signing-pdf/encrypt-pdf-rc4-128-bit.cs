using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // Source PDF
        const string outputPath = "encrypted.pdf";  // Encrypted PDF output
        const string userPassword  = "user123";     // Password required to open the PDF
        const string ownerPassword = "owner123";    // Password required to change permissions

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Restrict editing by allowing only printing (no modify, fill, etc.)
                Permissions permissions = Permissions.PrintDocument;

                // Apply RC4 128‑bit encryption
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.RC4x128);

                // Save the encrypted document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF encrypted successfully and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}