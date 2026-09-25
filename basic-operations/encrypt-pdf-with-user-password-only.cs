using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // User password that will be required to open the PDF
        const string userPassword = "viewOnly123";

        // Owner password (can be empty if not needed)
        const string ownerPassword = "";

        // Permissions: allow printing, but disallow editing and copying (extracting content)
        Permissions permissions = Permissions.PrintDocument;

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF and ensure deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Encrypt with user password only, using AES-256 encryption
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF encrypted successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}