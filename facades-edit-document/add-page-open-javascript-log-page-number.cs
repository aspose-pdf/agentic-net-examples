using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Copy the original PDF to a new file that will receive the JavaScript actions
        File.Copy(inputPath, outputPath, true);

        // Load the PDF document (Document implements IDisposable)
        using (Document doc = new Document(outputPath))
        {
            // JavaScript that logs the current page number to the console when the page is opened
            string js = "console.log('Page opened: ' + this.pageNum);";

            // Attach the JavaScript to the Open action of each page
            foreach (Page page in doc.Pages)
            {
                // Page‑level JavaScript actions are accessed via the `Actions` collection.
                // The correct property for the page‑open event is `OnOpen`.
                page.Actions.OnOpen = new JavascriptAction(js);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript added to all pages and saved as '{outputPath}'.");
    }
}
