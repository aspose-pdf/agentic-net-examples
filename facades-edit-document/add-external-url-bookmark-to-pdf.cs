using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string bookmarkTitle = "Example Site";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the facade and bind the existing PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Create a bookmark that opens an external URL when selected
        // Parameters: title, color, bold flag, italic flag, file (null for URI), action type, destination URL
        editor.CreateBookmarksAction(
            bookmarkTitle,
            Color.Blue,
            true,   // bold
            false,  // italic
            null,   // no file needed for URI action
            "URI",
            "https://example.org"
        );

        // Save the updated PDF
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPdf}'.");
    }
}