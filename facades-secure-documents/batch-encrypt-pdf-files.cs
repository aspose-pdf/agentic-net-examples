using System;
using System.IO;
using Aspose.Pdf;

class BatchEncryptPdf
{
    static void Main()
    {
        // Directory containing PDFs to encrypt
        const string sourceDir = @"C:\PdfFiles";
        // Directory where encrypted PDFs will be saved (can be the same as source)
        const string outputDir = @"C:\PdfFiles\Encrypted";

        // User and owner passwords used for all files
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Permissions to grant after encryption
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the source directory
        foreach (string inputPath in Directory.GetFiles(sourceDir, "*.pdf"))
        {
            try
            {
                // Determine output file path (same name, different folder)
                string fileName = Path.GetFileName(inputPath);
                string outputPath = Path.Combine(outputDir, fileName);

                // Load the PDF using Document (PdfFileEditor does not support BindPdf/Encrypt/Save)
                Document pdfDoc = new Document(inputPath);

                // Apply encryption – use Document.Encrypt for AES‑256
                pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF (overwrites if file exists)
                pdfDoc.Save(outputPath);

                Console.WriteLine($"Encrypted: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }
}
