using System;
using System.IO;
using Aspose.Pdf.Facades; // Aspose.Pdf Facades API

class Program
{
    static void Main()
    {
        // Paths to the encrypted input PDF and the decrypted output PDF.
        const string inputPdfPath  = @"C:\Files\encrypted.pdf";
        const string outputPdfPath = @"C:\Files\decrypted.pdf";

        // Owner password can be stored securely in Azure Key Vault, but for a self‑contained example we retrieve it from an environment variable.
        // In production replace this with Azure Key Vault access (add Azure.Identity & Azure.Security.KeyVault.Secrets packages).
        string ownerPassword = Environment.GetEnvironmentVariable("PDF_OWNER_PASSWORD");

        if (string.IsNullOrWhiteSpace(ownerPassword))
        {
            Console.Error.WriteLine("Owner password is not configured. Set the PDF_OWNER_PASSWORD environment variable.");
            return;
        }

        // Ensure the input file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize PdfFileSecurity with input and output file paths.
            // PdfFileSecurity implements IDisposable, so we use a using block to guarantee resource cleanup.
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPdfPath, outputPdfPath))
            {
                // Decrypt the PDF using the owner password.
                bool success = fileSecurity.DecryptFile(ownerPassword);

                if (success)
                {
                    Console.WriteLine($"PDF successfully decrypted to: {outputPdfPath}");
                }
                else
                {
                    Console.Error.WriteLine("Decryption failed. Verify the owner password.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
