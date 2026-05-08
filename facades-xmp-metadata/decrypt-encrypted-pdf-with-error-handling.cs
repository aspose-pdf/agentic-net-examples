using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "encrypted.pdf";   // Path to the source PDF (may be encrypted)
        const string outputPdf = "decrypted.pdf";   // Path where the decrypted PDF will be saved
        const string password  = "user123";        // Password for the encrypted PDF (user or owner)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfFileSecurity does NOT implement IDisposable, so we instantiate it directly.
        PdfFileSecurity security = new PdfFileSecurity();

        try
        {
            // Attempt to bind the PDF without a password.
            // If the file is encrypted, this call throws InvalidPasswordException.
            security.BindPdf(inputPdf);
        }
        catch (InvalidPasswordException)
        {
            // The PDF is encrypted. Bind it using a Document created with the password.
            // Document constructor overload (string fileName, string password) handles encrypted PDFs.
            using (Document doc = new Document(inputPdf, password))
            {
                security.BindPdf(doc);
            }
        }
        catch (Exception ex)
        {
            // Any other binding error is reported and the program exits.
            Console.Error.WriteLine($"Error binding PDF: {ex.Message}");
            return;
        }

        try
        {
            // Decrypt the bound PDF using the same password.
            // DecryptFile throws if the password is incorrect or decryption fails.
            security.DecryptFile(password);

            // Save the decrypted PDF to the desired output path.
            security.Save(outputPdf);
            Console.WriteLine($"Decrypted PDF saved to '{outputPdf}'.");
        }
        catch (InvalidPasswordException ex)
        {
            // Password was incorrect.
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error during decryption or saving.
            Console.Error.WriteLine($"Error during decryption/save: {ex.Message}");
        }
        finally
        {
            // Release any resources held by the facade.
            security.Close();
        }
    }
}