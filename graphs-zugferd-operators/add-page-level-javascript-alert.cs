using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // retained for potential future use, not required for this fix

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page number where the JavaScript should fire (1‑based indexing)
            const int targetPageNumber = 2;
            if (targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages.");
                return;
            }

            // Get the target page
            Page page = doc.Pages[targetPageNumber];

            // Assign a JavaScript action that will be executed when the page becomes visible.
            // For page‑level scripts Aspose.Pdf provides the OnOpen action (executed when the page is opened/displayed).
            page.Actions.OnOpen = new JavascriptAction(
                $"app.alert('You have reached page {targetPageNumber}.');");

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page‑level JavaScript: '{outputPath}'.");
    }
}
