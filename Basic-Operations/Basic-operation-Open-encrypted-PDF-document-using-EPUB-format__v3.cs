using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted_input.pdf";
        const string userPassword      = "user123";
        const string outputEpubPath    = "output.epub";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password constructor.
            using (Document pdfDoc = new Document(encryptedPdfPath, userPassword))
            {
                // Prepare EPUB save options (required for non‑PDF output).
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    // Optional: choose a content recognition mode.
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the document as EPUB.
                pdfDoc.Save(outputEpubPath, epubOptions);
            }

            Console.WriteLine($"Encrypted PDF successfully converted to EPUB: '{outputEpubPath}'.");
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