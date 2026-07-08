using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_continuous.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor does not implement IDisposable, so we manage its lifetime manually
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document into the editor
        editor.BindPdf(inputPath);

        // Set the viewer preference to continuous scrolling (one column layout)
        editor.ChangeViewerPreference(ViewerPreference.PageLayoutOneColumn);

        // Save the modified PDF with the new viewer preference
        editor.Save(outputPath);

        // Release resources held by the editor
        editor.Close();

        Console.WriteLine($"PDF saved with continuous view mode: '{outputPath}'.");
    }
}