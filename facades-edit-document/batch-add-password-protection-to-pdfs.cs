using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to protect
        const string inputFolder = "secure";
        // Folder where protected PDFs will be saved
        const string outputFolder = "secure_protected";
        // Identical user password to apply to all PDFs
        const string userPassword = "MySecretPassword";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            // Build output file name (original name + "_protected.pdf")
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_protected.pdf");

            try
            {
                // PdfFileSecurity handles encryption; the two‑argument constructor sets input and output files
                using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
                {
                    // Encrypt with the specified user password.
                    // Owner password is null → a random owner password will be generated.
                    // DocumentPrivilege.Print allows printing; adjust as needed.
                    // KeySize.x256 provides strong AES‑256 encryption.
                    bool success = security.EncryptFile(userPassword, null, DocumentPrivilege.Print, KeySize.x256);
                    if (!success)
                    {
                        Console.Error.WriteLine($"Encryption failed for: {inputPath}");
                    }
                }

                Console.WriteLine($"Encrypted PDF saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}