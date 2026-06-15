using System;
using System.Drawing;               // For Color and Rectangle
using Aspose.Pdf.Facades;          // Facade classes for bookmark and content editing

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // Existing PDF to modify
        const string outputPdf = "output_with_js_bookmarks.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor to add a bookmark that runs JavaScript when clicked
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // JavaScript code to be executed when the bookmark is activated
            string jsCode = "app.alert('Hello from JavaScript bookmark!');";

            // The destination for a JavaScript bookmark is a URI with the "javascript:" scheme
            string jsUri = "javascript:" + jsCode;

            // Create a bookmark with the desired title, color, style and JavaScript action
            // Action type "URI" tells the viewer to treat the destination as a URI.
            editor.CreateBookmarksAction(
                title:      "Run JavaScript",
                color:      Color.Blue,   // Bookmark title color
                boldFlag:   true,         // Bold title
                italicFlag: false,        // Not italic
                file:       null,         // Not needed for URI action
                actionType: "URI",        // Use URI action
                destination: jsUri);      // JavaScript URI

            // Save the modified PDF with the new bookmark
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with JavaScript bookmark: {outputPdf}");
    }
}