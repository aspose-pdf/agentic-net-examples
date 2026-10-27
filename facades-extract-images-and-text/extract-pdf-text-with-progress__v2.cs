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
        const int barWidth = 50;
        int filled = percent * barWidth / 100;
        string bar = new string('#', filled) + new string('-', barWidth - filled);
        Console.Write($"\r[{bar}] {percent}%");
        if (currentPage == totalPages)
            Console.WriteLine(); // move to next line when done
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output file is empty before appending page texts
        File.WriteAllText(outputTxt, string.Empty);

        // Get total page count via Document (PdfExtractor has no PageCount property)
        int pageCount;
        using (var doc = new Document(inputPdf))
        {
            pageCount = doc.Pages.Count;
        }

        // PdfExtractor implements IDisposable – wrap in using per lifecycle rule
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Process each page individually to report progress
            for (int page = 1; page <= pageCount; page++)
            {
                // Set the range to a single page using properties
                extractor.StartPage = page;
                extractor.EndPage   = page;

                // Extract text from the current page
                extractor.ExtractText();

                // Retrieve extracted text via stream overload
                string pageText;
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetText(ms);
                    ms.Position = 0;
                    using (StreamReader reader = new StreamReader(ms))
                    {
                        pageText = reader.ReadToEnd();
                    }
                }

                // Append page text to the output file
                File.AppendAllText(outputTxt, pageText);

                // Update progress bar
                ShowProgress(page, pageCount);
            }
        }

        Console.WriteLine($"Text extraction completed. Output saved to '{outputTxt}'.");
    }
}
