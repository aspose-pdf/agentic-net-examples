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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page numbers are 1‑based in Aspose.Pdf
            int pageNumber = 1; // change as needed

            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // JavaScript to display an alert when the user navigates to this page
            string js = $"app.alert('You have reached page {pageNumber}.');";

            // Create a JavaScript action (class name is case‑sensitive)
            JavascriptAction jsAction = new JavascriptAction(js);

            // Attach the script to the specified page using the page's Actions collection
            // OnOpen runs when the page becomes the current page (i.e., when the user reaches it)
            doc.Pages[pageNumber].Actions.OnOpen = jsAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page‑level JavaScript to '{outputPath}'.");
    }
}
