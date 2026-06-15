using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Create sample PDF files (self‑contained example)
        for (int i = 1; i <= 3; i++)
        {
            string sampleFile = $"sample{i}.pdf";
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(sampleFile);
            }
        }

        // Define the passwords to apply
        string userPassword = "user123";
        string ownerPassword = "owner123";

        // Get all PDF files in the current directory (evaluation mode limits to 4 files)
        string[] pdfFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.pdf");
        int maxFiles = Math.Min(pdfFiles.Length, 4);
        for (int index = 0; index < maxFiles; index++)
        {
            string filePath = pdfFiles[index];
            string fileName = Path.GetFileName(filePath);

            // Open the PDF using the full path to avoid relative‑path issues
            using (Document doc = new Document(filePath))
            {
                Permissions permissions = new Permissions(); // default permissions (all allowed)
                // Use a supported CryptoAlgorithm (AES‑128) – RC4_128 is not available in the current version
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx128);
                string encryptedFileName = $"encrypted_{fileName}";
                doc.Save(encryptedFileName);
            }
        }

        Console.WriteLine("Password protection applied to PDFs.");
    }
}
