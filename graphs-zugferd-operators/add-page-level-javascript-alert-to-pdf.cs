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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to attach the script (1‑based indexing)
            int pageNumber = 1;
            Page page = doc.Pages[pageNumber];

            // Assign JavaScript that runs when the page becomes visible
            page.Actions.OnOpen = new JavascriptAction($"app.alert('You have reached page {pageNumber}.');");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page‑level JavaScript to '{outputPath}'.");
    }
}
