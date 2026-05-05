using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that will receive the bookmarks
        const string inputPdf  = "input.pdf";
        // Output PDF with the newly added bookmarks
        const string outputPdf = "bookmarked_output.pdf";

        // Define bookmark titles and their corresponding web URLs
        var bookmarks = new (string Title, string Url)[]
        {
            ("Aspose Home",      "https://www.aspose.com"),
            ("GitHub Repository","https://github.com/aspose-pdf/Aspose.Pdf-for-.NET"),
            ("Documentation",    "https://docs.aspose.com/pdf/net/")
        };

        // Ensure the input file exists before proceeding
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use the PdfContentEditor facade to add bookmarks that open external URLs
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Create a bookmark for each entry; action type "URI" opens a web address
            foreach (var (title, url) in bookmarks)
            {
                // Parameters: title, color, boldFlag, italicFlag, file (null for URI), actionType, destination (URL)
                editor.CreateBookmarksAction(
                    title,
                    Color.Blue,   // bookmark title color
                    false,        // not bold
                    false,        // not italic
                    null,         // no external file needed for URI action
                    "URI",        // action type for external web link
                    url);         // target web address
            }

            // Save the modified PDF with the new bookmarks
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks with external URLs have been added to '{outputPdf}'.");
    }
}