using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // JavaScript that writes the current page number to the console
                string jsCode = $"app.console.println('Page {page.Number} opened');";

                // Assign the script to the page's OnOpen action
                page.Actions.OnOpen = new JavascriptAction(jsCode);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page‑open JavaScript actions to '{outputPath}'.");
    }
}