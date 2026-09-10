using System;
using System.IO;
using Aspose.Pdf;

class BatchEncryptPdf
{
    static void Main()
    {
        // Input folder containing PDFs
        const string inputFolder = @"C:\PdfFolder";
        // Output folder for encrypted PDFs
        const string outputFolder = @"C:\PdfFolder\Encrypted";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Derive password from file name (without extension)
                string password = Path.GetFileNameWithoutExtension(pdfPath);

                // Build output file path *before* opening the document so it remains in scope
                string outputPath = Path.Combine(outputFolder,
                    Path.GetFileNameWithoutExtension(pdfPath) + "_encrypted.pdf");

                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Define permissions (allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt with user and owner passwords set to the derived password
                    doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(pdfPath)} → {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
