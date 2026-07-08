using System;
using System.IO;
using Aspose.Pdf;

class BatchEncryptPdf
{
    static void Main()
    {
        // Folder containing PDF files to encrypt
        const string inputFolder = @"C:\PdfFolder";
        // Folder where encrypted PDFs will be saved
        const string outputFolder = @"C:\PdfFolder\Encrypted";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Enumerate all PDF files (case‑insensitive) in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Derive password from file name (without extension)
                string password = Path.GetFileNameWithoutExtension(pdfPath);

                // Define output file name
                string encryptedPath = Path.Combine(outputFolder,
                    Path.GetFileNameWithoutExtension(pdfPath) + "_encrypted.pdf");

                // Open the document, encrypt, and save
                using (Document doc = new Document(pdfPath))
                {
                    // Permissions: allow printing and content extraction (adjust as needed)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt with user and owner passwords (both set to the derived password)
                    doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);

                    // Save encrypted PDF
                    doc.Save(encryptedPath);
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(pdfPath)} → {encryptedPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }
}