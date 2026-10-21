using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // required by the task (not used directly but kept per specification)

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

        // Instantiate a Facades class as required (no disposal needed)
        var editor = new PdfFileEditor();

        // Load the PDF and ensure deterministic disposal
        using (var doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                var page = doc.Pages[i];

                // JavaScript that logs the current page number to the console on page open
                // In PDF JavaScript, 'this.pageNum' gives the page number (1‑based)
                // 'app.console.println' writes to the JavaScript console
                var js = new JavascriptAction("app.console.println('Page opened: ' + this.pageNum);");

                // Assign the action to the page's OnOpen event
                page.Actions.OnOpen = js;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}
