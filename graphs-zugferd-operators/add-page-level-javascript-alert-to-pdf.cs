using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";
        const int targetPageNumber = 2; // page to attach the JavaScript (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Validate page number
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Get the target page (1‑based indexing rule)
            Page page = doc.Pages[targetPageNumber];

            // Attach a page‑level JavaScript that shows an alert when the page becomes visible
            // Use the page's OnOpen action (the only supported page‑level JavaScript entry point)
            page.Actions.OnOpen = new JavascriptAction(
                $"app.alert('You have reached page {targetPageNumber}');");

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page‑level JavaScript: {outputPath}");
    }
}
