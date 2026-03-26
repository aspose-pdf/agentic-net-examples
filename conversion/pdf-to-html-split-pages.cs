using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "HtmlPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            using (Document pdfDoc = new Document(inputPath))
            {
                HtmlSaveOptions options = new HtmlSaveOptions
                {
                    SplitIntoPages = true
                };

                // The base file name; Aspose.Pdf will create separate HTML files for each page.
                string baseHtmlPath = Path.Combine(outputDir, "output.html");
                pdfDoc.Save(baseHtmlPath, options);
            }

            Console.WriteLine("PDF pages have been converted to individual HTML files.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}