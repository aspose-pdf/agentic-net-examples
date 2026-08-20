using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_js_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF using PdfContentEditor (facade API)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Create a bookmark that runs JavaScript when clicked.
        // Action type "URI" with a javascript: scheme triggers the script.
        // Use System.Drawing.Color for the color argument as required by the API.
        editor.CreateBookmarksAction(
            title: "Show Alert",                     // Bookmark title
            color: System.Drawing.Color.Red,          // Title colour (System.Drawing.Color)
            boldFlag: true,                           // Bold style
            italicFlag: false,                        // Italic style
            file: null,                               // Not used for URI action
            actionType: "URI",                       // Use URI action
            destination: "javascript:app.alert('Hello from bookmark!');" // JavaScript code
        );

        // Save the modified PDF
        editor.Save(outputPdf);

        Console.WriteLine($"PDF saved with JavaScript bookmark: {outputPdf}");
    }
}
