using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";          // original PDF
        const string encryptedPdf  = "encrypted.pdf";      // encrypted PDF
        const string decryptedPdf  = "decrypted.pdf";      // decrypted PDF (optional)
        const string outputSvg     = "output.svg";         // final SVG file

        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load the original PDF and encrypt it
            // -------------------------------------------------
            using (Document doc = new Document(inputPdf))
            {
                // Set permissions and encryption algorithm
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPdf);
            }

            // -------------------------------------------------
            // 2. Open the encrypted PDF with the user password,
            //    decrypt it, and optionally save the decrypted PDF
            // -------------------------------------------------
            using (Document encDoc = new Document(encryptedPdf, userPassword))
            {
                // Decrypt the document (no parameters)
                encDoc.Decrypt();

                // Save the decrypted PDF (optional, can be omitted)
                encDoc.Save(decryptedPdf);

                // -------------------------------------------------
                // 3. Convert the decrypted PDF to SVG format
                // -------------------------------------------------
                SvgSaveOptions svgOptions = new SvgSaveOptions(); // default options
                encDoc.Save(outputSvg, svgOptions);
            }

            Console.WriteLine("Encryption, decryption, and SVG conversion completed successfully.");
            Console.WriteLine($"Encrypted PDF: {encryptedPdf}");
            Console.WriteLine($"Decrypted PDF: {decryptedPdf}");
            Console.WriteLine($"SVG output:    {outputSvg}");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}