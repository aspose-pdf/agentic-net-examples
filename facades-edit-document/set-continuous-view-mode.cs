using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "continuous_view.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Create a PdfContentEditor facade, bind the PDF, set continuous view mode,
        // and save the result.
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document into the facade
        editor.BindPdf(inputPdf);

        // ViewerPreference.PageLayoutOneColumn enables continuous scrolling (single column layout)
        editor.ChangeViewerPreference(ViewerPreference.PageLayoutOneColumn);

        // Save the modified PDF
        editor.Save(outputPdf);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"PDF saved with continuous view mode: {outputPdf}");
    }
}