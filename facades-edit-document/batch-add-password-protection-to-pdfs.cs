using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to protect
        const string secureFolder = "secure";

        // The user password that will be applied to every PDF
        const string userPassword = "MySecretPassword";

        // Owner password can be null or empty – a random one will be generated
        const string ownerPassword = null;

        // Ensure the folder exists
        if (!Directory.Exists(secureFolder))
        {
            Console.Error.WriteLine($"Folder not found: {secureFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string inputFile in Directory.GetFiles(secureFolder, "*.pdf"))
        {
            try
            {
                // Build output file name (original name + "_protected.pdf")
                string outputFile = Path.Combine(
                    secureFolder,
                    Path.GetFileNameWithoutExtension(inputFile) + "_protected.pdf");

                // Initialize the facade
                PdfFileSecurity fileSecurity = new PdfFileSecurity();

                // Bind the source PDF
                fileSecurity.BindPdf(inputFile);

                // Encrypt the PDF with the specified user password,
                // random owner password, Print privilege and 256‑bit AES encryption
                fileSecurity.EncryptFile(
                    userPassword,
                    ownerPassword,
                    DocumentPrivilege.Print,
                    KeySize.x256);

                // Save the encrypted PDF
                fileSecurity.Save(outputFile);

                // Release resources held by the facade
                fileSecurity.Close();

                Console.WriteLine($"Encrypted: {Path.GetFileName(inputFile)} → {Path.GetFileName(outputFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{Path.GetFileName(inputFile)}': {ex.Message}");
            }
        }
    }
}