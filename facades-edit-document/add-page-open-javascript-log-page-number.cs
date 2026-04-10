using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // Facades namespace is required by the task

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Add a JavaScript action to each page that runs when the page is opened.
            // The script writes the current page number (zero‑based) to the PDF console.
            for (int i = 1; i <= doc.Pages.Count; i++) // page indexing is 1‑based
            {
                JavascriptAction jsAction = new JavascriptAction("app.console.println('Page ' + this.pageNum);");
                // Correct property for page‑level actions is "Actions", not "PageAction"
                doc.Pages[i].Actions.OnOpen = jsAction;
            }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with page‑open JavaScript saved to '{outputPath}'.");
    }
}
