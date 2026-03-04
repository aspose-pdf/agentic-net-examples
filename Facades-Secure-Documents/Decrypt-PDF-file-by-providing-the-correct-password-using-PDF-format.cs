using System;
using Aspose.Pdf.Facades;   // Facade classes for PDF security operations

class Program
{
    static void Main()
    {
        // Paths for the encrypted source PDF and the decrypted output PDF
        const string inputPath  = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";

        // Owner (or user) password required to open the encrypted document
        const string password = "ownerpass";

        // Verify that the input file exists before proceeding
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileSecurity implements IDisposable via SaveableFacade, so use a using block
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Bind the encrypted PDF file to the facade
            security.BindPdf(inputPath);

            // Decrypt the bound document using the provided password.
            // DecryptFile returns true on success; false indicates failure (e.g., wrong password).
            bool decrypted = security.DecryptFile(password);
            if (!decrypted)
            {
                Console.Error.WriteLine("Decryption failed – check the password.");
                return;
            }

            // Save the decrypted PDF to the desired output location.
            security.Save(outputPath);
        }

        Console.WriteLine($"Decryption completed. Decrypted file saved to '{outputPath}'.");
    }
}