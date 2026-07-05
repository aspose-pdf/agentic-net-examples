using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (a Facades class) to modify viewer preferences
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Hide the menu bar when the PDF is displayed
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with HideMenubar preference: {outputPath}");
    }
}