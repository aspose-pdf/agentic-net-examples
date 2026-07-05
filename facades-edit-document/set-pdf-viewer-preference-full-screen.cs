using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "fullScreen_output.pdf";

        // Verify that the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfContentEditor facade and bind the PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Set the viewer preference to full‑screen mode
        editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"PDF saved with full‑screen viewer preference: {outputPath}");
    }
}