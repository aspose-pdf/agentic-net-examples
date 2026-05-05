using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (a Facade) to modify viewer preferences
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPath);

            // Set the document title (this is the text that will appear in the window caption)
            editor.Document.SetTitle("My PDF Title");

            // Enable the DisplayDocTitle flag so the viewer shows the title in the window bar
            editor.ChangeViewerPreference(ViewerPreference.DisplayDocTitle);

            // Save the modified PDF to the output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with DisplayDocTitle enabled: {outputPath}");
    }
}