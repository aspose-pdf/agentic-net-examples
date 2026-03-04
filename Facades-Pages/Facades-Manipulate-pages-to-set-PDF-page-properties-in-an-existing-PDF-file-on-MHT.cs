using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory that contains the source MHT file.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input MHT file and output PDF file paths.
        string mhtPath = Path.Combine(dataDir, "input.mht");
        string outputPdf = Path.Combine(dataDir, "output.pdf");

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load the MHT file into a PDF Document using MhtLoadOptions.
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document pdfDoc = new Document(mhtPath, loadOptions))
        {
            // Use PdfPageEditor (a facade) to modify page properties.
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor.
                pageEditor.BindPdf(pdfDoc);

                // Set desired page properties.
                // Rotate all pages 90 degrees.
                pageEditor.Rotation = 90;

                // Zoom pages to 150% (1.5x).
                pageEditor.Zoom = 1.5f;

                // Change page size to A4.
                pageEditor.PageSize = PageSize.A4;

                // Set display duration (seconds) for each page.
                pageEditor.DisplayDuration = 5;

                // Apply the changes to the document.
                pageEditor.ApplyChanges();

                // Save the modified PDF to the output path.
                pageEditor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}