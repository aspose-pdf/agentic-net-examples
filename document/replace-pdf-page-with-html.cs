using System;
using System.IO;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        const string inputPdfPath = "source.pdf";
        const string htmlPath = "page.html";
        const string outputPdfPath = "result.pdf";
        const int pageToReplace = 2; // 1‑based page index

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the original PDF document
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            // Convert the HTML file to a temporary PDF document (single page)
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                if (htmlDoc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("HTML conversion produced no pages.");
                    return;
                }

                // Delete the page that will be replaced
                sourceDoc.Pages.Delete(pageToReplace);

                // Insert the generated page at the same position
                sourceDoc.Pages.Insert(pageToReplace, htmlDoc.Pages[1]);
            }

            // Save the updated PDF
            sourceDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page {pageToReplace} replaced and saved to '{outputPdfPath}'.");
    }
}
