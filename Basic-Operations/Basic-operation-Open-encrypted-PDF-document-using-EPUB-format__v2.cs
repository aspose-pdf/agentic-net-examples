using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, EpubSaveOptions, etc.)

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted.pdf";   // Path to the password‑protected PDF
        const string outputPath = "output.epub";     // Desired EPUB output file
        const string password   = "user123";         // User (open) password for the PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF by supplying the user password.
            // Document(string, string) constructor handles password‑protected files.
            using (Document doc = new Document(inputPath, password))
            {
                // Configure EPUB save options.
                // ContentRecognitionMode is a nested enum; use the fully qualified name.
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the document as EPUB.  Passing the options ensures the correct format.
                doc.Save(outputPath, epubOptions);
            }

            Console.WriteLine($"Successfully converted '{inputPath}' to EPUB → '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}