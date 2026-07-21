using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a minimal PDF so the sandbox has a file to work with.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a single blank page – enough for the demo.
            seed.Pages.Add();
            seed.Save(inputPath);
        }

        // ---------------------------------------------------------------------
        // 2. Load the PDF, set its document title, and save to the output file.
        // ---------------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Set the title that will appear in the window caption when the flag is on.
            doc.SetTitle("My PDF Title");
            doc.Save(outputPath);
        }

        // ---------------------------------------------------------------------
        // 3. Change the viewer preference so the title is displayed in the window bar.
        // ---------------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(outputPath); // load the PDF file into the facade
            editor.ChangeViewerPreference(ViewerPreference.DisplayDocTitle); // enable flag
            editor.Save(outputPath); // overwrite the file with the updated preference
        }
    }
}
