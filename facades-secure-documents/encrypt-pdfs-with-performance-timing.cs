using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to encrypt
        string[] inputFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };

        // Passwords for encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Determine output path
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_encrypted.pdf");

            // Measure encryption time
            Stopwatch timer = Stopwatch.StartNew();

            // Use PdfFileSecurity facade to encrypt the PDF
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                // Bind the source PDF file
                security.BindPdf(inputPath);

                // Encrypt with desired privileges and key size (AES 256-bit)
                security.EncryptFile(
                    userPassword,
                    ownerPassword,
                    DocumentPrivilege.Print,
                    KeySize.x256);

                // Save the encrypted PDF
                security.Save(outputPath);
            }

            timer.Stop();

            Console.WriteLine($"Encrypted '{inputPath}' to '{outputPath}' in {timer.ElapsedMilliseconds} ms.");
        }
    }
}