using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // Path to the PDF to be processed
        const string outputPdf = "output.pdf";     // Path where the decrypted PDF will be saved
        const string password  = "userPassword";  // Password for the encrypted PDF (if known)

        // Facade for handling PDF security (encryption/decryption)
        PdfFileSecurity securityFacade = new PdfFileSecurity();

        try
        {
            // Attempt to bind the PDF without providing a password.
            // If the file is not encrypted this succeeds.
            securityFacade.BindPdf(inputPdf);
        }
        catch (InvalidPasswordException)
        {
            // The PDF is encrypted and requires a password.
            // Re‑bind using the supplied password.
            // PdfFileSecurity does not have a BindPdf overload that accepts a password,
            // so we load the document with the password using the core API and then bind it.
            using (Document doc = new Document(inputPdf, password))
            {
                securityFacade.BindPdf(doc);
            }
        }
        catch (Exception ex)
        {
            // Any other error (file not found, corrupted, etc.) is reported here.
            Console.Error.WriteLine($"Failed to bind PDF: {ex.Message}");
            return;
        }

        try
        {
            // Decrypt the bound PDF and save the result.
            // DecryptFile writes an unencrypted copy to the specified path.
            securityFacade.DecryptFile(outputPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Decryption failed: {ex.Message}");
        }
        finally
        {
            // Close the facade to release resources.
            securityFacade.Close();
        }

        Console.WriteLine($"Decrypted PDF saved to '{outputPdf}'.");
    }
}