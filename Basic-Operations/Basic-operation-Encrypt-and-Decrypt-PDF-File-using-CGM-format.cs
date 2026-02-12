using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Input CGM file and intermediate/output paths
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";

        // Verify the CGM source exists
        if (!File.Exists(cgmPath))
        {
            Console.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Load CGM and convert to PDF
        // -----------------------------------------------------------------
        CgmLoadOptions loadOptions = new CgmLoadOptions();               // create load options
        Document pdfDoc = new Document(cgmPath, loadOptions);            // load CGM as PDF

        // Save the plain PDF (document-save rule)
        pdfDoc.Save(pdfPath);

        // -----------------------------------------------------------------
        // Encrypt the PDF
        // -----------------------------------------------------------------
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Define permissions (e.g., allow printing and content extraction)
        Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

        // Encrypt using RC4 128‑bit algorithm
        pdfDoc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.RC4x128);

        // Save the encrypted PDF (document-save rule)
        pdfDoc.Save(encryptedPath);

        // -----------------------------------------------------------------
        // Decrypt the PDF
        // -----------------------------------------------------------------
        // Load the encrypted PDF providing the user password
        Document encryptedDoc = new Document(encryptedPath, userPassword);

        // Decrypt (parameter‑less overload)
        encryptedDoc.Decrypt();

        // Save the decrypted PDF (document-save rule)
        encryptedDoc.Save(decryptedPath);

        Console.WriteLine("CGM conversion, encryption, and decryption completed successfully.");
    }
}