using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputDirectory = "input";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Ensure the input directory exists
        Directory.CreateDirectory(inputDirectory);

        // Process each PDF file in the directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        foreach (string pdfFilePath in pdfFiles)
        {
            string outputFileName = Path.GetFileNameWithoutExtension(pdfFilePath) + "_enc.pdf";

            using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
            {
                // Load the source PDF
                fileSecurity.BindPdf(pdfFilePath);

                // Encrypt with user/owner passwords, allow printing, use 256‑bit AES encryption
                fileSecurity.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);

                // Save the encrypted PDF (output file name only, saved to the current working directory)
                fileSecurity.Save(outputFileName);
            }

            Console.WriteLine($"Encrypted: {outputFileName}");
        }

        Console.WriteLine("Batch encryption completed.");
    }
}