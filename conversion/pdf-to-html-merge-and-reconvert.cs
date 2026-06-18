using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdfPath      = "input.pdf";
        const string combinedHtmlPath  = "combined.html";
        const string outputPdfPath     = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // List to collect HTML markup of each page
        List<string> pageHtmls = new List<string>();

        // Convert PDF to HTML – one HTML file per page
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            // Generate separate HTML for each page
            SplitIntoPages = true,

            // Capture each page's HTML markup via custom strategy
            CustomHtmlSavingStrategy = new HtmlSaveOptions.HtmlPageMarkupSavingStrategy(info =>
            {
                // Ensure the stream is at the beginning
                info.ContentStream.Position = 0;
                using (var reader = new StreamReader(info.ContentStream))
                {
                    pageHtmls.Add(reader.ReadToEnd());
                }
            })
        };

        // The actual file name passed here is irrelevant because we handle saving ourselves
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            pdfDoc.Save("temp.html", htmlOptions);
        }

        // Combine all page HTML fragments into a single HTML document
        string combinedHtml = "<!DOCTYPE html>\n<html>\n<head>\n<meta charset=\"UTF-8\" />\n<title>Combined Document</title>\n</head>\n<body>\n"
                            + string.Join("\n", pageHtmls)
                            + "\n</body>\n</html>";

        File.WriteAllText(combinedHtmlPath, combinedHtml);
        Console.WriteLine($"Combined HTML saved to '{combinedHtmlPath}'.");

        // Load the combined HTML and convert it back to PDF
        HtmlLoadOptions loadOptions = new HtmlLoadOptions(); // default base path
        using (Document htmlDoc = new Document(combinedHtmlPath, loadOptions))
        {
            htmlDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Final PDF saved to '{outputPdfPath}'.");
    }
}