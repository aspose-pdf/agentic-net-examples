using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to encrypt
        string[] inputFiles = { "input1.pdf", "input2.pdf" };
        // Directory for encrypted output files
        string outputDir = "Encrypted";
        Directory.CreateDirectory(outputDir);

        foreach (string inFile in inputFiles)
        {
            if (!File.Exists(inFile))
            {
                Console.Error.WriteLine($"File not found: {inFile}");
                continue;
            }

            string outFile = Path.Combine(outputDir,
                Path.GetFileNameWithoutExtension(inFile) + "_encrypted.pdf");

            // Start timing
            Stopwatch sw = Stopwatch.StartNew();

            // Initialize the facade and bind the source PDF
            PdfFileSecurity fileSecurity = new PdfFileSecurity();
            fileSecurity.BindPdf(inFile);

            // Set desired privileges (e.g., allow printing) and encryption strength
            DocumentPrivilege privilege = DocumentPrivilege.Print;
            // Encrypt using 256‑bit AES (KeySize.x256)
            bool success = fileSecurity.EncryptFile(
                userPassword: "user123",
                ownerPassword: "owner123",
                privilege: privilege,
                keySize: KeySize.x256);

            // Save the encrypted PDF to the output path
            if (success)
            {
                fileSecurity.Save(outFile);
            }

            // Stop timing
            sw.Stop();

            if (success)
            {
                Console.WriteLine($"Encrypted '{inFile}' to '{outFile}' in {sw.ElapsedMilliseconds} ms.");
            }
            else
            {
                Console.Error.WriteLine($"Encryption failed for '{inFile}'.");
            }
        }
    }
}