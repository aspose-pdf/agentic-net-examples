using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            extractor.ExtractText(); // enable text extraction

            int pageIndex = 1;
            while (extractor.HasNextPageText())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Write the current page's text into the memory stream.
                    extractor.GetNextPageText(ms);
                    // Convert the stream bytes to a string (UTF‑8 is the default for Aspose).
                    string pageText = Encoding.UTF8.GetString(ms.ToArray());

                    string outPath = Path.Combine(outputDir, $"Page_{pageIndex}.txt");
                    File.WriteAllText(outPath, pageText ?? string.Empty);
                }
                pageIndex++;
            }
        }

        Console.WriteLine("Text extraction completed.");
    }
}
