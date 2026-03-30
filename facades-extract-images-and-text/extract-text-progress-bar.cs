using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDirectory = "ExtractedPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Load the document to obtain total page count
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count;

            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPath);
            extractor.ExtractText();

            int processedPages = 0;
            while (extractor.HasNextPageText())
            {
                processedPages++;
                string outputPath = Path.Combine(outputDirectory, $"Page_{processedPages}.txt");
                extractor.GetNextPageText(outputPath);
                ShowProgress(processedPages, totalPages);
            }
        }

        Console.WriteLine();
        Console.WriteLine("Extraction completed.");
    }

    static void ShowProgress(int processed, int total)
    {
        double percent = (double)processed / total * 100.0;
        int barWidth = 50;
        int filled = (int)(percent / 100.0 * barWidth);
        string bar = new string('#', filled) + new string('-', barWidth - filled);
        Console.Write($"\rProgress: [{bar}] {percent:0.00}%");
    }
}