using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "protected.pdf";      // original encrypted PDF
        const string userPassword   = "user123";            // password to open the PDF
        const string ownerPassword  = "owner123";           // owner password for re‑encryption
        const string outputPath     = "reprotected.pdf";    // final encrypted PDF

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load the encrypted PDF using the user password
            // -------------------------------------------------
            // Document(string fileName, string password) loads a protected PDF.
            using (Document doc = new Document(inputPath, userPassword))
            {
                // -------------------------------------------------
                // 2. Update Creator metadata (DocumentInfo.Creator)
                // -------------------------------------------------
                doc.Info.Creator = "MyApplication v1.0";

                // -------------------------------------------------
                // 3. Re‑encrypt the PDF with desired permissions
                // -------------------------------------------------
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                // Document.Encrypt(userPassword, ownerPassword, permissions, algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the re‑encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Successfully re‑encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
