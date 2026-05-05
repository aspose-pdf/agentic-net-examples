using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (facade) to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Create a bookmark that executes JavaScript when clicked
            // Action type "JavaScript" with the script code as destination
            editor.CreateBookmarksAction(
                title: "Show Alert",
                color: Color.Blue,
                boldFlag: true,
                italicFlag: false,
                file: null,                     // not needed for JavaScript action
                actionType: "JavaScript",       // specifies a JavaScript action
                destination: "app.alert('Hello from bookmark!');"
            );

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks with JavaScript saved to '{outputPath}'.");
    }
}