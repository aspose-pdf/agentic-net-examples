using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Input CGM file path
        const string cgmPath = "input.cgm";
        // Paths for the intermediate encrypted PDF and the final decrypted PDF
        const string encryptedPdfPath = "encrypted.pdf";
        const string decryptedPdfPath = "decrypted.pdf";

        // Passwords for encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        try
        {
            // Verify the CGM source file exists
            if (!File.Exists(cgmPath))
                throw new FileNotFoundException($"CGM file not found: {cgmPath}");

            // Load the CGM file into a PDF document using default CgmLoadOptions
            var loadOptions = new CgmLoadOptions();
            Document pdfDoc = new Document(cgmPath, loadOptions);

            // Define permissions for the encrypted document (example: allow printing and content extraction)
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the PDF with the specified passwords and permissions
            pdfDoc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.RC4x128);

            // Save the encrypted PDF
            pdfDoc.Save(encryptedPdfPath);

            // Load the encrypted PDF using the user password
            Document encryptedDoc = new Document(encryptedPdfPath, userPassword);

            // Decrypt the document (no parameters needed after loading with password)
            encryptedDoc.Decrypt();

            // Save the decrypted PDF
            encryptedDoc.Save(decryptedPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}