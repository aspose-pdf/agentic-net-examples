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
        const int targetPageNumber = 2; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} does not exist.");
                return;
            }

            // Get the target page (Aspose.Pdf pages are 1‑based)
            Page page = doc.Pages[targetPageNumber];

            // Assign JavaScript that runs when the page becomes visible (opened)
            page.Actions.OnOpen = new JavascriptAction(
                $"app.alert('You have reached page {targetPageNumber}.');");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page‑level JavaScript: {outputPath}");
    }
}
