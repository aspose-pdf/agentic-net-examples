using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the PDF file exists before creating PdfFileInfo.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: File '{inputPath}' does not exist.");
            return;
        }

        try
        {
            // Initialize the PdfFileInfo facade with the PDF file.
            // The facade implements IDisposable, so we wrap it in a using block.
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
            {
                // Retrieve standard metadata fields. Use null‑coalescing to avoid printing empty strings.
                string title    = pdfInfo.Title    ?? "(none)";
                string author   = pdfInfo.Author   ?? "(none)";
                string subject  = pdfInfo.Subject  ?? "(none)";
                string keywords = pdfInfo.Keywords ?? "(none)";

                // Output the metadata.
                Console.WriteLine($"Title   : {title}");
                Console.WriteLine($"Author  : {author}");
                Console.WriteLine($"Subject : {subject}");
                Console.WriteLine($"Keywords: {keywords}");
            }
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Pdf specific or IO exceptions and report them.
            Console.WriteLine($"Failed to read PDF metadata: {ex.Message}");
        }
    }
}