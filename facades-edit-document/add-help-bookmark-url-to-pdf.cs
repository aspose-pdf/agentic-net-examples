using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // PDF with new bookmark
        const string helpUrl   = "https://docs.aspose.com/pdf/net/"; // online documentation

        // Ensure the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (Facade) to add a bookmark that opens a URL
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Create a bookmark titled "Help" that launches the URL.
        // Parameters: title, color, bold, italic, file (null for URI), action type, destination URL
        editor.CreateBookmarksAction(
            "Help",
            System.Drawing.Color.Blue,
            true,               // bold title
            false,              // not italic
            null,               // no external file needed for URI action
            "URI",              // action type for opening a web link
            helpUrl);

        // Save the modified PDF
        editor.Save(outputPdf);

        Console.WriteLine($"Bookmark 'Help' added. Output saved to '{outputPdf}'.");
    }
}