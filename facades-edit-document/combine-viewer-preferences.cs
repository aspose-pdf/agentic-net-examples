using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF into the PdfContentEditor facade
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);

            // Combine viewer preferences: center the window and hide the toolbar
            editor.ChangeViewerPreference(ViewerPreference.CenterWindow);
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with combined viewer preferences to '{outputPdf}'.");
    }
}