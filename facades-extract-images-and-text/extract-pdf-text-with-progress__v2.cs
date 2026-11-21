using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Simple console progress bar
    static void ShowProgress(int processed, int total)
    {
        int percent = (int)((processed / (double)total) * 100);
        Console.Write($"\rProcessing pages: {processed}/{total} ({percent}%)");
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTxt = "extracted_text.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Use Document to obtain the total page count (PdfExtractor has no GetPageCount())
            int totalPages;
            using (var doc = new Document(inputPdf))
            {
                totalPages = doc.Pages.Count;
            }

            var sb = new StringBuilder();

            // Extract text page‑by‑page to report progress
            for (int page = 1; page <= totalPages; page++)
            {
                using (var extractor = new PdfExtractor())
                {
                    extractor.BindPdf(inputPdf);
                    // Set the page range via StartPage / EndPage (ExtractPage method does not exist)
                    extractor.StartPage = page;
                    extractor.EndPage   = page;
                    extractor.ExtractText();

                    // GetText requires a destination stream
                    using (var ms = new MemoryStream())
                    {
                        extractor.GetText(ms);
                        ms.Position = 0;
                        using (var reader = new StreamReader(ms))
                        {
                            string pageText = reader.ReadToEnd();
                            sb.AppendLine(pageText);
                        }
                    }
                }

                ShowProgress(page, totalPages);
            }

            Console.WriteLine(); // move to next line after progress bar

            // Save the accumulated text
            File.WriteAllText(outputTxt, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"Extraction complete. Text saved to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
