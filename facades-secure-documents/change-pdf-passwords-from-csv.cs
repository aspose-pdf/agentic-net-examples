using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is used as required

class Program
{
    // CSV format: PdfPath,NewUserPassword,NewOwnerPassword
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: ChangePdfPasswords <csvFilePath>");
            return;
        }

        string csvPath = args[0];
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Read all lines, ignore empty lines
        var lines = File.ReadAllLines(csvPath);
        foreach (var rawLine in lines)
        {
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            // Split by comma – simple CSV handling (no quoted commas)
            var parts = rawLine.Split(',');
            if (parts.Length < 3)
            {
                Console.Error.WriteLine($"Invalid line (expected 3 columns): {rawLine}");
                continue;
            }

            string pdfPath      = parts[0].Trim();
            string newUserPwd   = parts[1].Trim();
            string newOwnerPwd  = parts[2].Trim();

            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF not found: {pdfPath}");
                continue;
            }

            try
            {
                // Load the PDF document first – this gives us page count and encryption status
                using (Document doc = new Document(pdfPath))
                {
                    Console.WriteLine($"Processing: {pdfPath}");
                    Console.WriteLine($"  Pages: {doc.Pages.Count}");
                    Console.WriteLine($"  Encrypted: {doc.IsEncrypted}");

                    // Define permissions you want to keep (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt with the new passwords using AES-256 (recommended)
                    doc.Encrypt(newUserPwd, newOwnerPwd, perms, CryptoAlgorithm.AESx256);

                    // Overwrite the original file
                    doc.Save(pdfPath);
                }

                Console.WriteLine("  Passwords updated successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"  Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
