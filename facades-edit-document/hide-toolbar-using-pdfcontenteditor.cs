using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Create a minimal PDF if it does not already exist in the sandbox.
        if (!System.IO.File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add(); // add a blank page
                seed.Save(inputPath);
            }
        }

        // Initialize the PDF content editor facade and set viewer preference.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            // Hide the toolbar when the PDF is opened.
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Toolbar hidden PDF saved to '{outputPath}'.");
    }
}
