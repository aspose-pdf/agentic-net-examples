using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing PDFs to encrypt
        const string inputDirectory = @"C:\PdfFiles";
        // Output directory for encrypted PDFs
        const string outputDirectory = @"C:\PdfFiles\Encrypted";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Passwords to apply to all files
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Enumerate all PDF files in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Build output file name (same name with "_encrypted" suffix)
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDirectory, $"{fileName}_encrypted.pdf");

            try
            {
                // PdfFileSecurity implements IDisposable, so use a using block
                using (PdfFileSecurity security = new PdfFileSecurity())
                {
                    // Bind the source PDF file
                    security.BindPdf(inputPath);

                    // Encrypt the file with desired privileges and key size
                    // DocumentPrivilege.Print is an example; adjust as needed
                    security.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);

                    // Save the encrypted PDF to the output location
                    security.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{inputPath}': {ex.Message}");
            }
        }
    }
}