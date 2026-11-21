using System;
using System.IO;
using Aspose.Pdf;               // Document class for loading/saving PDFs
using Aspose.Pdf.Facades;      // PdfFileInfo for quick encryption check

class Program
{
    static void Main()
    {
        const string inputPath      = "encrypted_input.pdf";
        const string decryptedPath  = "decrypted_output.pdf";
        const string userPassword   = "user123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Quick check – does the file report being encrypted?
            PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
            if (!fileInfo.IsEncrypted)
            {
                Console.WriteLine("The PDF is not encrypted – no decryption needed.");
                File.Copy(inputPath, decryptedPath, overwrite: true);
                return;
            }

            // Load the encrypted PDF with the supplied password and re‑save it without any password.
            // Aspose.Pdf provides a Document constructor that accepts the password directly.
            using (Document pdfDoc = new Document(inputPath, userPassword))
            {
                // Saving without specifying a password writes an un‑encrypted PDF.
                pdfDoc.Save(decryptedPath);
            }

            Console.WriteLine($"Decryption successful. Saved to '{decryptedPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Incorrect password: {ex.Message}");
        }
        catch (PdfException ex)
        {
            // PdfException is also thrown when the file is encrypted but the password is missing/invalid.
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
