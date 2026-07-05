using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "single_page_layout.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use PdfContentEditor to modify viewer preferences
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Set the viewer preference to single‑page layout
                editor.ChangeViewerPreference(ViewerPreference.PageLayoutSinglePage);

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with single‑page layout: '{outputPath}'.");
    }
}