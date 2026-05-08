using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Assign JavaScript that runs when the document is opened.
            // This script hides the toolbar and the menu bar in the PDF viewer.
            doc.OpenAction = new JavascriptAction("app.hideToolbar(); app.hideMenuBar();");

            // Additionally set the viewer preference flags directly (optional but reinforces the behavior)
            doc.HideToolBar = true;
            doc.HideMenubar = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden toolbar and menu bar: {outputPath}");
    }
}
