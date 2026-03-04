using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string epubOutputPath = "output.epub";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the user password
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Decrypt the document (no parameters required)
                doc.Decrypt();

                // Configure EPUB save options (optional: set recognition mode)
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the document as EPUB; explicit SaveOptions are required for non‑PDF formats
                doc.Save(epubOutputPath, epubOptions);
            }

            Console.WriteLine($"Encrypted PDF opened and saved as EPUB: '{epubOutputPath}'");
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