using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // required for Color

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

        // Use PdfContentEditor (facade) to add a bookmark that runs JavaScript
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF document
            editor.BindPdf(inputPath);

            // Create a bookmark with a JavaScript action.
            // Parameters:
            //   title        – displayed bookmark title
            //   color        – colour of the title (System.Drawing.Color)
            //   boldFlag     – true for bold text
            //   italicFlag   – false for normal style
            //   file         – not required for JavaScript (null)
            //   actionType   – "JavaScript" indicates a JS action
            //   destination  – the JavaScript code to execute
            editor.CreateBookmarksAction(
                "Show Alert",                                   // title
                Color.Blue,                                     // title colour
                true,                                           // bold
                false,                                          // italic
                null,                                           // file (unused)
                "JavaScript",                                   // action type
                "app.alert('Hello from Aspose.Pdf!');"          // JavaScript code
            );

            // Save the modified PDF with the new bookmark
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript bookmark saved to '{outputPath}'.");
    }
}