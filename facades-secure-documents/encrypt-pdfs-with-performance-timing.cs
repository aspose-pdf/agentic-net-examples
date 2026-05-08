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
        // Corresponding output files
        string[] outputFiles = { "output1.pdf", "output2.pdf", "output3.pdf" };

        // Encryption parameters
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        DocumentPrivilege privilege = DocumentPrivilege.Print; // allow printing only
        KeySize keySize = KeySize.x256; // AES 256‑bit encryption

        for (int i = 0; i < inputFiles.Length; i++)
        {
            string inPath = inputFiles[i];
            string outPath = outputFiles[i];

            if (!File.Exists(inPath))
            {
                Console.Error.WriteLine($"File not found: {inPath}");
                continue;
            }

            try
            {
                // Initialize the facade and bind the source PDF
                using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
                {
                    fileSecurity.BindPdf(inPath);

                    // Measure encryption time
                    Stopwatch sw = Stopwatch.StartNew();

                    // Perform encryption
                    bool success = fileSecurity.EncryptFile(userPassword, ownerPassword, privilege, keySize);
                    sw.Stop();

                    if (!success)
                    {
                        Console.Error.WriteLine($"Encryption failed for: {inPath}");
                        continue;
                    }

                    // Save the encrypted PDF
                    fileSecurity.Save(outPath);

                    Console.WriteLine($"Encrypted '{inPath}' to '{outPath}' in {sw.Elapsed.TotalMilliseconds} ms.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inPath}': {ex.Message}");
            }
        }
    }
}