using System;
using System.IO;
using System.Drawing;               // Required for Color parameters
using Aspose.Pdf.Facades;          // Facade classes for bookmark manipulation

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PDF content editor facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Create a bookmark that opens the Aspose website
        editor.CreateBookmarksAction(
            title:      "Aspose Home",          // Bookmark title
            color:      Color.Blue,            // Title colour
            boldFlag:   true,                  // Bold title
            italicFlag: false,                 // Not italic
            file:       null,                  // No additional file needed for URI action
            actionType: "URI",                 // Action type for external URL
            destination:"https://www.aspose.com" // Target web address
        );

        // Create another bookmark that opens the GitHub homepage
        editor.CreateBookmarksAction(
            title:      "GitHub",
            color:      Color.Green,
            boldFlag:   false,
            italicFlag: true,
            file:       null,
            actionType: "URI",
            destination:"https://github.com"
        );

        // Save the modified PDF with the new bookmarks
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPdf}'.");
    }
}