using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor and ViewerPreference are in this namespace

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "continuous_view.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the PdfContentEditor facade, bind the source PDF,
        // set the viewer preference to continuous (one‑column) layout,
        // and save the result.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // ViewerPreference.PageLayoutOneColumn enables continuous scrolling
        editor.ChangeViewerPreference(ViewerPreference.PageLayoutOneColumn);

        // Save the modified PDF
        editor.Save(outputPdf);

        Console.WriteLine($"PDF saved with continuous view mode: {outputPdf}");
    }
}