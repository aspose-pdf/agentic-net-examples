using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        string svgPath = "input.svg";
        string encryptedPdfPath = "encrypted.pdf";
        string decryptedPdfPath = "decrypted.pdf";

        // Verify SVG source exists
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        try
        {
            // Load SVG into a new PDF document
            Document pdfDoc = new Document(svgPath, new SvgLoadOptions());

            // Encrypt the PDF with user and owner passwords
            string userPassword = "user123";
            string ownerPassword = "owner123";
            Permissions permissions = Permissions.PrintDocument | Permissions.ModifyContent;
            pdfDoc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.RC4x128);

            // Save the encrypted PDF
            pdfDoc.Save(encryptedPdfPath);

            // Load the encrypted PDF using the user password
            Document encryptedDoc = new Document(encryptedPdfPath, userPassword);

            // Decrypt the PDF (removes encryption)
            encryptedDoc.Decrypt();

            // Save the decrypted PDF
            encryptedDoc.Save(decryptedPdfPath);

            Console.WriteLine("Encryption and decryption completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}