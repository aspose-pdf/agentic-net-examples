using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "encrypted.pdf";   // Encrypted PDF file
        const string userPassword = "user123";    // Password for the PDF
        const string outputEpub = "output.epub";  // Desired EPUB output

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password
            using (Document doc = new Document(inputPdf, userPassword))
            {
                // Configure EPUB save options (use flow recognition for better reflow)
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the document as EPUB
                doc.Save(outputEpub, epubOptions);
            }

            Console.WriteLine($"Encrypted PDF successfully converted to EPUB: '{outputEpub}'");
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