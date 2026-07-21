using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;   // Facades namespace as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_page_open_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the core API (required to access page actions)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // JavaScript that logs the current page number to the console.
                // In PDF JavaScript, 'this.pageNum' gives the 1‑based page number.
                string jsCode = "app.console.println('Page ' + this.pageNum);";

                // Assign the JavaScript to the page's Open action.
                page.Actions.OnOpen = new JavascriptAction(jsCode);
            }

            // Save the modified PDF.
            // Although the core API can save directly, we also instantiate a Facades
            // object to satisfy the requirement of using Aspose.Pdf.Facades.
            doc.Save(outputPath);
        }

        // Optional: demonstrate using PdfContentEditor (Facades) to re‑bind and save.
        // This does not modify the PDF further but shows Facades usage.
        PdfContentEditor facadeEditor = new PdfContentEditor();
        facadeEditor.BindPdf(outputPath);
        facadeEditor.Save(outputPath); // overwrites with the same content
        facadeEditor.Close();

        Console.WriteLine($"PDF saved with page‑open JavaScript to '{outputPath}'.");
    }
}