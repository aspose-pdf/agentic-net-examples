using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "centered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and edit its viewer preferences
        using (Document doc = new Document(inputPath))
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(doc);
            // Enable CenterWindow flag so the PDF opens centered on screen
            editor.ChangeViewerPreference(ViewerPreference.CenterWindow);
            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with CenterWindow enabled at '{outputPath}'.");
    }
}