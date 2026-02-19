using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF and the output EPUB file
        const string pdfPath = "encrypted.pdf";
        const string epubPath = "output.epub";

        // Password for the encrypted PDF (user password)
        const string pdfPassword = "userPassword";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the encrypted PDF using the password constructor
            // Document(string, string) opens a password‑protected PDF.
            Document pdfDocument = new Document(pdfPath, pdfPassword);

            // Prepare EPUB save options. The RecognitionMode property is optional;
            // if you need a specific mode, use the correct enum value:
            //   RecognitionMode = Aspose.Pdf.EpubSaveOptions.RecognitionMode.PdfFlow
            // Here we omit it to avoid the enum‑reference compile issue.
            EpubSaveOptions saveOptions = new EpubSaveOptions();

            // Save the document as EPUB. The overload with SaveOptions is used.
            pdfDocument.Save(epubPath, saveOptions);

            Console.WriteLine($"Encrypted PDF successfully converted to EPUB: '{epubPath}'.");
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("Error: The provided password is incorrect or the PDF is not password protected.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
