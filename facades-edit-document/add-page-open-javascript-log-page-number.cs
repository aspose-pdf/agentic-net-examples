using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // Facades namespace imported as requested

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPath))
        {
            // Add a JavaScript action to each page that logs the page number when the page is opened
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // this.pageNum is zero‑based; add 1 for a human‑readable page number
                string js = "app.console.println('Page opened: ' + (this.pageNum + 1));";
                doc.Pages[i].Actions.OnOpen = new JavascriptAction(js);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page‑open JavaScript to '{outputPath}'.");
    }
}
