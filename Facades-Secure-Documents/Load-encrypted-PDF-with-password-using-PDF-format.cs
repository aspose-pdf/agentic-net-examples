using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF and the output (decrypted) PDF
        const string inputPath  = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";

        // Password required to open the encrypted PDF (user or owner password)
        const string password = "myPassword";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfFileSecurity (a Facade) to bind, decrypt, and save the PDF
        using (PdfFileSecurity pdfSecurity = new PdfFileSecurity())
        {
            // Bind the encrypted PDF file to the facade
            pdfSecurity.BindPdf(inputPath);

            // Decrypt the PDF using the provided password.
            // DecryptFile accepts the owner password; if the document has no owner password,
            // the user password can be used as well.
            bool success = pdfSecurity.DecryptFile(password);
            if (!success)
            {
                Console.Error.WriteLine("Failed to decrypt the PDF. Check the password.");
                return;
            }

            // Save the decrypted PDF to the specified output path
            pdfSecurity.Save(outputPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
    }
}