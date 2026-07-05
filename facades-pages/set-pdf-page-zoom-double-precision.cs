using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "zoomed_output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PDF page editor facade
        PdfPageEditor editor = new PdfPageEditor();

        // Load the PDF document into the editor
        editor.BindPdf(inputPath);

        // Desired zoom factor (double precision for accuracy)
        double zoomFactor = 2.5; // 250 % scaling

        // Assign to the Zoom property (expects float, so cast)
        editor.Zoom = (float)zoomFactor;

        // Apply the zoom change to all pages
        editor.ApplyChanges();

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Applied zoom of {zoomFactor * 100}% and saved to '{outputPath}'.");
    }
}