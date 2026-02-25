using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including EpubSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string userPassword      = "user123";
        const string outputEpubPath    = "output.epub";

        // Verify input file exists
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password; wrap in using for deterministic disposal
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Prepare EPUB save options (no need to set properties unless desired)
                EpubSaveOptions epubOptions = new EpubSaveOptions();

                // Save the document as EPUB; passing SaveOptions ensures non‑PDF output
                doc.Save(outputEpubPath, epubOptions);
            }

            Console.WriteLine($"Encrypted PDF successfully converted to EPUB: {outputEpubPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}