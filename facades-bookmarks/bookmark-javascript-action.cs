using System;
using System.IO;
using System.Drawing; // Added for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the existing PDF document
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Create a bookmark that executes JavaScript via a URI action.
        // NOTE: The color parameter must be a System.Drawing.Color, not Aspose.Pdf.Color.
        editor.CreateBookmarksAction(
            "Show Alert",
            System.Drawing.Color.Red, // Fixed type
            true,   // isBold
            false,  // isItalic
            null,   // style (null for default)
            "URI",
            "javascript:app.alert('Hello from bookmark!');"
        );

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Bookmark with JavaScript saved to '{outputPath}'.");
    }
}
