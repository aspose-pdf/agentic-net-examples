using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath  = "example.pdf";
        const string outputPath = "example_out.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document into the editor
        editor.BindPdf(inputPath);

        // Change viewer preferences as required
        // Hide the menu bar and disable any page mode (use none)
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
        editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);

        // Save the modified PDF
        editor.Save(outputPath);

        // Optionally retrieve the current viewer preference flags
        int prefValue = editor.GetViewerPreference();
        Console.WriteLine($"Viewer preferences applied (flags value): {prefValue}");
    }
}