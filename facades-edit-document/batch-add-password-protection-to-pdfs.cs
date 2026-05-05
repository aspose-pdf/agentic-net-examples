using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;               // DocumentPrivilege enum
using Aspose.Pdf.Security;    // KeySize and Algorithm enums

class Program
{
    static void Main()
    {
        // Folder that contains the PDFs to protect
        const string folderPath = "secure";

        // The user password that will be applied to every PDF
        const string userPassword = "MySecretPassword";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            // Create an output file name – you can overwrite the original if desired
            string outputPath = Path.Combine(
                folderPath,
                Path.GetFileNameWithoutExtension(inputPath) + "_protected.pdf");

            try
            {
                // Initialise the facade
                PdfFileSecurity fileSecurity = new PdfFileSecurity();

                // Bind the source PDF
                fileSecurity.BindPdf(inputPath);

                // Encrypt the file:
                // - userPassword: the password all users must provide
                // - ownerPassword: null → a random owner password will be generated
                // - privilege: set desired access rights (Print in this example)
                // - keySize: 256‑bit encryption (AES)
                // - algorithm: AES (required when using 256‑bit key)
                fileSecurity.EncryptFile(
                    userPassword,
                    null,
                    DocumentPrivilege.Print,
                    KeySize.x256,
                    Algorithm.AES);

                // Save the encrypted PDF
                fileSecurity.Save(outputPath);

                Console.WriteLine($"Encrypted PDF saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
