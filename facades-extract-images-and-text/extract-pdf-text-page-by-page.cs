using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor from Aspose.Pdf.Facades to extract text page by page
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // Prepare for text extraction (Unicode encoding is default)
            extractor.ExtractText();

            int pageNumber = 1;
            // Loop through all pages and save each page's text to a separate .txt file
            while (extractor.HasNextPageText())
            {
                string outputFile = Path.Combine(outputFolder, $"Page_{pageNumber}.txt");
                extractor.GetNextPageText(outputFile);
                pageNumber++;
            }
        }

        Console.WriteLine($"Text extraction complete. Files saved in '{outputFolder}'.");
    }
}