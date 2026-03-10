using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string epubPath = "input.epub";

        // Verify that the EPUB file exists before attempting to load it.
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"Error: EPUB file not found at path '{epubPath}'. Please provide a valid file.");
            return;
        }

        try
        {
            // Load the EPUB as a PDF document (in-memory) using the appropriate load options.
            using (Document pdfDoc = new Document(epubPath, new EpubLoadOptions()))
            {
                // PdfFileInfo works on a Document instance and provides access to PDF metadata.
                PdfFileInfo fileInfo = new PdfFileInfo(pdfDoc);

                // Retrieve the required metadata fields.
                string title    = fileInfo.Title ?? string.Empty;
                string author   = fileInfo.Author ?? string.Empty;
                string subject  = fileInfo.Subject ?? string.Empty;
                string keywords = fileInfo.Keywords ?? string.Empty;

                // Output the metadata.
                Console.WriteLine($"Title   : {title}");
                Console.WriteLine($"Author  : {author}");
                Console.WriteLine($"Subject : {subject}");
                Console.WriteLine($"Keywords: {keywords}");
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected exceptions (e.g., corrupted EPUB, load failures) and report them.
            Console.Error.WriteLine($"An error occurred while processing the EPUB: {ex.Message}");
        }
    }
}
