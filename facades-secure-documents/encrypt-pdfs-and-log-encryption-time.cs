using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;                     // DocumentPrivilege class
using Aspose.Pdf.Facades;            // PdfFileSecurity, KeySize

class Program
{
    // Passwords and encryption settings (adjust as required)
    private const string userPassword  = "user123";
    private const string ownerPassword = "owner123";
    // DocumentPrivilege is a class with static members, so it cannot be const.
    // Use static readonly (or var) instead.
    private static readonly DocumentPrivilege privilege = DocumentPrivilege.Print;
    private const KeySize keySize = KeySize.x256; // enum – const is fine

    static void Main()
    {
        // Input PDF files to encrypt
        string[] inputFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf",
            // add more file paths as needed
        };

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Prepare output path
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_encrypted.pdf");

            // Measure encryption time
            Stopwatch sw = Stopwatch.StartNew();

            // PdfFileSecurity implements IDisposable via SaveableFacade, so use using
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                // Bind the source PDF
                security.BindPdf(inputPath);

                // Perform encryption
                // EncryptFile throws on failure; returns true on success
                bool success = security.EncryptFile(userPassword, ownerPassword, privilege, keySize);
                if (!success)
                {
                    Console.Error.WriteLine($"Encryption failed for: {inputPath}");
                    continue;
                }

                // Save the encrypted PDF to the desired location
                security.Save(outputPath);
            }

            sw.Stop();
            Console.WriteLine($"Encrypted '{inputPath}' -> '{outputPath}' in {sw.ElapsedMilliseconds} ms.");
        }
    }
}
