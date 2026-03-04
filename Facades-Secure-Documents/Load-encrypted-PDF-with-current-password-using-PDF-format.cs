using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF and the output (decrypted) PDF
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";

        // The password that protects the PDF (user or owner password)
        const string password = "currentPassword";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        try
        {
            // Create the PdfFileSecurity facade
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                // Load (bind) the encrypted PDF file
                security.BindPdf(encryptedPath);

                // Decrypt the PDF using the provided password.
                // DecryptFile works with owner password; if the document has no owner password,
                // it will accept the user password as well.
                bool success = security.DecryptFile(password);
                if (!success)
                {
                    Console.Error.WriteLine("Decryption failed. Check the password.");
                    return;
                }

                // Save the decrypted PDF to a new file
                security.Save(decryptedPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}