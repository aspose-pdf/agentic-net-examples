using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade namespace usage

class Program
{
    static void Main()
    {
        // Folder containing PDFs to protect
        const string secureFolder = "secure";

        // Identical user password for all PDFs
        const string userPassword = "MySecurePassword";

        // Owner password can be empty or a separate value
        const string ownerPassword = "";

        if (!Directory.Exists(secureFolder))
        {
            Console.Error.WriteLine($"Folder not found: {secureFolder}");
            return;
        }

        // Instantiate a Facade class (required by the task)
        // No disposable pattern needed for PdfFileEditor
        PdfFileEditor facade = new PdfFileEditor();

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(secureFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Define permissions (adjust as needed)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Apply encryption with AES-256
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Overwrite the original file with the encrypted version
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }
    }
}