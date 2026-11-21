using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Actions live in the Annotations namespace

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
            // JavaScript that runs when the document is opened.
            // app.toolbar = 0  hides the toolbar.
            // app.menuBar = 0 hides the menu bar.
            string js = "app.toolbar = 0; app.menuBar = 0;";

            // Assign the JavaScript as the document's OpenAction
            doc.OpenAction = new JavascriptAction(js); // Note the lowercase 's' in JavascriptAction

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with viewer‑preference JavaScript to '{outputPath}'.");
    }
}