using System;
using System.IO;
using Aspose.Pdf; // EpubSaveOptions resides in this namespace

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path (replace with your actual file)
        const string inputPdfPath = "input.pdf";
        // Output EPUB file path
        const string outputEpubPath = "output.epub";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Open the PDF file as a read‑only stream
            using (FileStream pdfStream = File.OpenRead(inputPdfPath))
            {
                // Load the PDF document from the stream
                Document pdfDocument = new Document(pdfStream);

                // Prepare EPUB save options (default options are sufficient for a basic conversion)
                var epubOptions = new EpubSaveOptions();

                // Save the document as EPUB using the specified options
                pdfDocument.Save(outputEpubPath, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: {outputEpubPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
