using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted.pdf";   // Path to the encrypted PDF
        const string outputPath = "decrypted.pdf";   // Path where the decrypted PDF will be saved
        const string password   = "userpass";        // Current user or owner password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileSecurity implements IDisposable, so use a using block
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                // Load (bind) the encrypted PDF file
                security.BindPdf(inputPath);

                // Decrypt the file using the supplied password.
                // Returns true on success; false indicates an invalid password.
                bool decrypted = security.DecryptFile(password);
                if (!decrypted)
                {
                    Console.Error.WriteLine("Decryption failed – invalid password.");
                    return;
                }

                // Save the now‑decrypted PDF to the desired location
                security.Save(outputPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}