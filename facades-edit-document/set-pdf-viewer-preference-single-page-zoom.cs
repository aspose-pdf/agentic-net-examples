using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Change viewer preference to single page layout
            PdfContentEditor viewerPrefEditor = new PdfContentEditor();
            viewerPrefEditor.BindPdf(doc);
            viewerPrefEditor.ChangeViewerPreference(ViewerPreference.PageLayoutSinglePage);

            // Set default zoom to 100% (1.0f) using PdfPageEditor
            PdfPageEditor zoomEditor = new PdfPageEditor();
            zoomEditor.BindPdf(doc);
            zoomEditor.Zoom = 1.0f; // 100% zoom

            // Save the modified document (both changes are applied)
            zoomEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated viewer preferences to '{outputPath}'.");
    }
}