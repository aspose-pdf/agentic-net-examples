using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";
        const string ownerPassword = "ownerPwd";

        // Verify that the encrypted PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileSecurity handles encryption/decryption of PDF files.
        // The constructor takes the source file and the destination file.
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
        {
            // DecryptFile returns true if decryption succeeds.
            bool result = fileSecurity.DecryptFile(ownerPassword);

            if (result)
                Console.WriteLine($"Decryption succeeded. Decrypted file saved as '{outputPath}'.");
            else
                Console.WriteLine("Decryption failed.");
        }
    }
}