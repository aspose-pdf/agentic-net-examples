using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Simple console progress bar
    static void ShowProgress(int currentPage, int totalPages)
    {
        int percent = (int)((double)currentPage / totalPages * 100);
        Console.CursorLeft = 0;
        Console.Write($"Processing page {currentPage}/{totalPages} ({percent}% )");
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTextFolder = "ExtractedText";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputTextFolder);

        // Load the document to obtain total page count
        using (Document doc = new Document(inputPdfPath))
        {
            int totalPages = doc.Pages.Count;

            // Initialize the extractor
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPdfPath);
            extractor.StartPage = 1;
            extractor.EndPage = totalPages;
            extractor.ExtractText(); // Prepare extraction

            int currentPage = 0;
            while (extractor.HasNextPageText())
            {
                currentPage++;
                // Extract text of the current page to a file
                string pageTextPath = Path.Combine(outputTextFolder, $"Page_{currentPage}.txt");
                extractor.GetNextPageText(pageTextPath);

                // Update progress UI
                ShowProgress(currentPage, totalPages);
            }

            Console.WriteLine(); // Move to next line after progress bar
        }

        Console.WriteLine("Text extraction completed.");
    }
}