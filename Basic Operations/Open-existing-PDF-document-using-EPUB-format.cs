using System;
using System.IO;
using Aspose.Pdf;               // Core PDF classes
// No additional using directives for saving options; they are in Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Desired output EPUB file path
        const string epubPath = "output.epub";

        // Verify the source file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Open the existing PDF document (wrapped in a using block for deterministic disposal)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Initialize EPUB save options (default constructor)
                EpubSaveOptions epubOptions = new EpubSaveOptions();

                // Optional: set a title for the generated EPUB
                epubOptions.Title = Path.GetFileNameWithoutExtension(pdfPath);

                // Save the PDF as an EPUB file using the explicit save options
                pdfDocument.Save(epubPath, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: '{epubPath}'");
        }
        catch (Exception ex)
        {
            // Handle any errors (e.g., file access, conversion issues)
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}