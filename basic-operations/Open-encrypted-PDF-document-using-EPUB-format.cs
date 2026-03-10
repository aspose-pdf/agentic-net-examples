using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string userPassword      = "user123";
        const string epubOutputPath    = "output.epub";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF with the user password
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Remove encryption before saving
                doc.Decrypt();

                // Configure EPUB save options (use flow recognition for better layout)
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the document as EPUB
                doc.Save(epubOutputPath, epubOptions);
            }

            Console.WriteLine($"Encrypted PDF opened and saved as EPUB: '{epubOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}