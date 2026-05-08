using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "centered_output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfContentEditor facade, bind the PDF, set the CenterWindow flag, and save
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);                                 // Load the PDF
        editor.ChangeViewerPreference(ViewerPreference.CenterWindow); // Enable CenterWindow
        editor.Save(outputPath);                                   // Persist changes
        editor.Close();                                            // Release resources

        Console.WriteLine($"PDF saved with CenterWindow=true to '{outputPath}'.");
    }
}