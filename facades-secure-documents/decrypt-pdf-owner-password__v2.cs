using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Paths to the encrypted input PDF and the decrypted output PDF.
        // Output path must be a simple filename as per requirements.
        string inputPath = "encrypted.pdf";
        string outputPath = "decrypted.pdf";

        // Retrieve the owner password from a secure location.
        // In a real scenario this could be fetched from Azure Key Vault.
        // Here we read it from an environment variable for demonstration.
        string ownerPassword = Environment.GetEnvironmentVariable("PDF_OWNER_PASSWORD");
        if (string.IsNullOrEmpty(ownerPassword))
        {
            Console.Error.WriteLine("Owner password not found in environment variable 'PDF_OWNER_PASSWORD'.");
            return;
        }

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfFileSecurity (parameterless constructor) to decrypt the PDF.
        using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
        {
            // Bind the encrypted PDF.
            fileSecurity.BindPdf(inputPath);

            // Attempt decryption with the owner password.
            bool decrypted = fileSecurity.DecryptFile(ownerPassword);
            if (!decrypted)
            {
                Console.Error.WriteLine("Decryption failed. The provided password may be incorrect.");
                return;
            }

            // Save the decrypted PDF.
            fileSecurity.Save(outputPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
    }
}