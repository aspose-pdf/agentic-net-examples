using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "encrypted_input.pdf";
        const string outputPdf = "decrypted_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // First try to load the PDF without a password.
        Document pdfDoc = null;
        try
        {
            pdfDoc = new Document(inputPdf);
            // If loading succeeds, the PDF is not encrypted.
            Console.WriteLine("PDF loaded successfully – no encryption detected.");
        }
        catch (InvalidPasswordException ex)
        {
            // The PDF is encrypted and requires a password.
            Console.Error.WriteLine($"Load failed: the document is encrypted. {ex.Message}");

            // Replace "myPassword" with the actual password if it is known.
            const string userPassword = "myPassword";
            try
            {
                // Attempt to load the PDF using the supplied password.
                pdfDoc = new Document(inputPdf, userPassword);
                // Save an unprotected copy.
                pdfDoc.Save(outputPdf);
                Console.WriteLine($"Decrypted PDF saved to '{outputPdf}'.");
            }
            catch (InvalidPasswordException innerEx)
            {
                // Password was incorrect or not provided.
                Console.Error.WriteLine($"Decryption failed: invalid password. {innerEx.Message}");
            }
            catch (Exception innerEx)
            {
                // Other errors during decryption.
                Console.Error.WriteLine($"Unexpected error during decryption: {innerEx.Message}");
            }
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors during loading.
            Console.Error.WriteLine($"Error loading PDF: {ex.Message}");
        }
        finally
        {
            // Ensure the Document is disposed if it was created.
            if (pdfDoc != null)
                pdfDoc.Dispose();
        }
    }
}
