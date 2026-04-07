using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "form.pdf";
        const string outputPath = "form_zoomed.pdf";
        const double zoomFactor = 1.5; // 150% zoom when the PDF is opened

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF using FormEditor (facade API)
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Access the underlying Document instance
            Document doc = editor.Document; // FormEditor exposes the Document object

            // Create a destination that points to the first page with the desired zoom
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            XYZExplicitDestination zoomDest = new XYZExplicitDestination(firstPage, 0, 0, zoomFactor);

            // Set the document's OpenAction so the viewer jumps to the destination on open
            doc.OpenAction = new GoToAction(zoomDest);

            // Save the modified PDF (FormEditor handles disposal of the document)
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with zoom factor {zoomFactor} to '{outputPath}'.");
    }
}