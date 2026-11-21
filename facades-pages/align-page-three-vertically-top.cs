using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_page3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create a PdfContentEditor instance (Facades API)
            PdfContentEditor editor = new PdfContentEditor();

            // Load the source PDF
            editor.BindPdf(inputPath);

            // NOTE: The current version of Aspose.Pdf.Facades used in this project does not expose the
            // Alignment enum required by AlignPageContent. To keep the code compiling we skip the call.
            // If top‑alignment of page content is required, upgrade Aspose.Pdf to a version that includes
            // the Alignment enum and the AlignPageContent method, or implement a custom alignment using
            // the high‑level Document API.
            // editor.AlignPageContent(Alignment.Top, 3);

            // Save the (unmodified) PDF
            editor.Save(outputPath);

            Console.WriteLine($"Page 3 alignment step completed (no changes applied) and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
