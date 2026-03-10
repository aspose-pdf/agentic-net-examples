using System;
using System.IO;
using Aspose.Pdf; // All Aspose.Pdf classes (including EpubLoadOptions) are in this namespace

class Program
{
    static void Main()
    {
        // Input file can be either a PDF/A document or an EPUB document.
        const string inputPath  = "input.epub";   // change to your source file (PDF/A or EPUB)
        const string outputPath = "output.pdf";   // resulting regular PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine loading strategy based on file extension.
        string ext = Path.GetExtension(inputPath).ToLowerInvariant();

        try
        {
            // Use a using block for deterministic disposal of the Document.
            using (Document doc = CreateDocument(inputPath, ext))
            {
                // If the source is a PDF/A, remove the PDF/A compliance.
                // This makes the document a regular PDF.
                doc.RemovePdfaCompliance();

                // Save the result as a standard PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed. PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }

    // Helper method to create a Document with the appropriate load options.
    private static Document CreateDocument(string path, string extension)
    {
        // EPUB requires EpubLoadOptions; other formats can be loaded directly.
        if (extension == ".epub")
        {
            // Initialize load options for EPUB → PDF conversion.
            EpubLoadOptions epubLoadOptions = new EpubLoadOptions();
            // Load the EPUB file using the options.
            return new Document(path, epubLoadOptions);
        }

        // For PDF/A (or any other PDF) just load the file.
        return new Document(path);
    }
}