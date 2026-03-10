using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Create the facade and bind the encrypted PDF
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                security.BindPdf(inputPath);

                // Decrypt using the owner (or user) password
                bool decrypted = security.DecryptFile(ownerPassword);
                if (!decrypted)
                {
                    Console.Error.WriteLine("Decryption failed. Verify the password.");
                    return;
                }

                // Save the decrypted document, preserving PDF compliance
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