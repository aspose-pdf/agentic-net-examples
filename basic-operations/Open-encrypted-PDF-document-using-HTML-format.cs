using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for HtmlSaveOptions dependencies

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string password          = "user123";
        const string htmlOutputPath    = "output.html";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password
            using (Document doc = new Document(encryptedPdfPath, password))
            {
                // Initialize HTML save options (required for non‑PDF output)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                // HTML conversion relies on GDI+; handle Windows‑only limitation gracefully
                try
                {
                    doc.Save(htmlOutputPath, htmlOptions);
                    Console.WriteLine($"HTML saved to '{htmlOutputPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}