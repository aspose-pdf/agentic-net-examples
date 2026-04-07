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
        int pageNumber = 1; // 1‑based page index

        Document document;

        // If the source PDF does not exist, create a minimal one on‑the‑fly.
        if (!File.Exists(inputPath))
        {
            document = new Document();
            // Add a blank page (or you could add some placeholder content).
            document.Pages.Add();
            // Save the temporary file so that subsequent code works with a real file path.
            document.Save(inputPath);
        }
        else
        {
            document = new Document(inputPath);
        }

        // Ensure the requested page exists.
        if (document.Pages.Count >= pageNumber)
        {
            Page page = document.Pages[pageNumber];
            // JavaScript that shows an alert when the page is opened.
            string js = $"app.alert('You have reached page {pageNumber}.');";
            JavascriptAction jsAction = new JavascriptAction(js);
            // Attach the action to the page Open event.
            page.Actions.OnOpen = jsAction;
        }
        else
        {
            Console.WriteLine($"The document has only {document.Pages.Count} page(s); cannot attach script to page {pageNumber}.");
        }

        // Save the modified PDF.
        document.Save(outputPath);
        Console.WriteLine($"JavaScript added to page {pageNumber} and saved as {outputPath}");
    }
}
