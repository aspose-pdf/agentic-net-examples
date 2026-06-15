using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted_input.pdf";
        const string outputPath = "decrypted_output.pdf";
        const string password   = "userPassword"; // user or owner password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileSecurity is a facade that can bind, decrypt and save PDF files.
        // It implements IDisposable, so we wrap it in a using block.
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            try
            {
                // Attempt to bind the PDF without a password.
                // If the file is not encrypted this succeeds.
                security.BindPdf(inputPath);
            }
            catch (InvalidPasswordException)
            {
                // The PDF is encrypted and requires a password.
                // Load the document with the supplied password and re‑bind.
                try
                {
                    using (Document doc = new Document(inputPath, password))
                    {
                        security.BindPdf(doc);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to open encrypted PDF: {ex.Message}");
                    return;
                }
            }
            catch (Exception ex)
            {
                // Any other binding error (corrupt file, unsupported format, etc.)
                Console.Error.WriteLine($"BindPdf failed: {ex.Message}");
                return;
            }

            try
            {
                // Decrypt the bound PDF and write the result to outputPath.
                // DecryptFile uses the owner password if present; otherwise the user password.
                security.DecryptFile(outputPath);
                Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Decryption failed: {ex.Message}");
            }
        }
    }
}