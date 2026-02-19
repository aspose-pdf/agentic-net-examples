using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Path to the encrypted PDF file.
        string pdfPath = "encrypted.pdf";

        // Password required to open the PDF (user password).
        string userPassword = "myPassword";

        // Path where the TeX output will be saved.
        string texOutputPath = "output.tex";

        // Verify that the input file exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password.
            // The constructor Document(string, string) loads a password‑protected PDF.
            using (Document pdfDocument = new Document(pdfPath, userPassword))
            {
                // Save the document to TeX format.
                // TeXSaveOptions provides options for the conversion.
                TeXSaveOptions texOptions = new TeXSaveOptions();

                pdfDocument.Save(texOutputPath, texOptions);
            }

            Console.WriteLine($"Successfully saved TeX file to '{texOutputPath}'.");
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("Error: The provided password is incorrect.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}