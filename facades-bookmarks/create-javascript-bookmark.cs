using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Create a minimal source PDF so the sandbox has a file to open.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a single blank page (required for most PDF operations).
            seed.Pages.Add();
            seed.Save(inputPath);
        }

        // Initialize the content editor facade and bind the source PDF.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Create a bookmark titled "Show Alert" that runs JavaScript when clicked.
            // Parameters:
            //   title      – bookmark title
            //   color      – title colour (System.Drawing.Color)
            //   boldFlag   – make title bold
            //   italicFlag – make title italic
            //   file       – not used for JavaScript (null)
            //   actionType – "JavaScript" to indicate a script action
            //   destination– the JavaScript code to execute
            editor.CreateBookmarksAction(
                "Show Alert",
                System.Drawing.Color.Red, // fully qualified to avoid ambiguity
                true,
                false,
                null,
                "JavaScript",
                "app.alert('Hello from bookmark!');"
            );

            // Save the modified PDF with the new interactive bookmark.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript bookmark saved to '{outputPath}'.");
    }
}
