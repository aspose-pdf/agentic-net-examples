using System;
using System.Drawing;               // Required for Color parameters in Facades API
using Aspose.Pdf.Facades;          // Facade classes for bookmark and content editing

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // Source PDF
        const string outputPdf = "output.pdf";  // PDF with JavaScript‑enabled bookmarks

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use the Facade API to add a bookmark that runs JavaScript when clicked
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Create a bookmark titled "Show Alert". The bookmark will appear in bold,
            // use a blue title colour, and execute the supplied JavaScript code.
            // Parameters:
            //   title        – bookmark title
            //   color        – title colour (System.Drawing.Color)
            //   boldFlag     – true => bold title
            //   italicFlag   – false => normal style
            //   file         – not needed for JavaScript actions (null)
            //   actionType   – "JavaScript" indicates a JS action
            //   destination  – the JavaScript code to execute
            editor.CreateBookmarksAction(
                title:       "Show Alert",
                color:       Color.Blue,
                boldFlag:    true,
                italicFlag:  false,
                file:        null,
                actionType:  "JavaScript",
                destination: "app.alert('Bookmark clicked!');"
            );

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with JavaScript bookmark saved to '{outputPdf}'.");
    }
}