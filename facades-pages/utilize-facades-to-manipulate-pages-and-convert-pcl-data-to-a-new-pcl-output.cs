using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPclPath  = "input.pcl";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPclPath}");
            return;
        }

        // Load the PCL file into a PDF Document using PclLoadOptions (PCL is input‑only).
        PclLoadOptions pclLoadOptions = new PclLoadOptions();

        using (Document pdfDoc = new Document(inputPclPath, pclLoadOptions))
        {
            // Manipulate pages with the PdfPageEditor facade.
            using (PdfPageEditor pageEditor = new PdfPageEditor(pdfDoc))
            {
                // Example manipulations:
                // Rotate every page 90 degrees.
                pageEditor.Rotation = 90;
                // Reduce page size to 80% (zoom factor 0.8).
                pageEditor.Zoom = 0.8f;
                // Apply the changes to the underlying document.
                pageEditor.ApplyChanges();
            }

            // Save the result. PCL cannot be saved directly (no PclSaveOptions), so we save as PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
    }
}