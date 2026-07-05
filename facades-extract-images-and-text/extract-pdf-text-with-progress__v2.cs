using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputFolder = "ExtractedPages";    // folder for per‑page text files

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF to obtain the total page count (required for progress calculation)
        using (Document doc = new Document(inputPdfPath))
        {
            int totalPages = doc.Pages.Count;
            if (totalPages == 0)
            {
                Console.WriteLine("The document contains no pages.");
                return;
            }

            // Initialise the extractor and bind it to the loaded document
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(doc);
                extractor.StartPage = 1;          // start from first page
                extractor.EndPage   = totalPages; // process all pages
                extractor.ExtractText();          // prepare text extraction

                int currentPage = 1;
                while (extractor.HasNextPageText())
                {
                    // Build output file name for the current page
                    string outFile = Path.Combine(outputFolder, $"Page_{currentPage}.txt");

                    // Extract the current page's text to a file
                    extractor.GetNextPageText(outFile);

                    // Update progress bar
                    int percent = (int)((double)currentPage / totalPages * 100);
                    Console.Write($"\rProcessing page {currentPage}/{totalPages} – {percent}% completed");

                    currentPage++;
                }

                Console.WriteLine(); // move to next line after progress output
            }
        }

        Console.WriteLine("Text extraction completed.");
    }
}