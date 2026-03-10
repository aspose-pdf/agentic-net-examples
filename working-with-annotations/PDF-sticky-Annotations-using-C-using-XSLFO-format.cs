using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for input XSL‑FO, output PDF, and XFDF files
        const string xslFoPath   = "input.fo";
        const string pdfPath     = "output.pdf";
        const string xfdfPath    = "annotations.xfdf";
        const string importedPdf = "imported.pdf";

        // Ensure the XSL‑FO source exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO file and create a PDF document
        XslFoLoadOptions loadOptions = new XslFoLoadOptions(xslFoPath);
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Add a sticky (rubber‑stamp) annotation on the first page
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 250, 550);
            StampAnnotation stamp = new StampAnnotation(pdfDoc.Pages[1], rect)
            {
                // Use a built‑in stamp icon; other options include custom images
                Icon = StampIcon.NotForPublicRelease,
                Color = Aspose.Pdf.Color.Red,
                Contents = "Sticky note"
            };
            pdfDoc.Pages[1].Annotations.Add(stamp);

            // Save the PDF (PDF format, no SaveOptions needed)
            pdfDoc.Save(pdfPath);

            // Export all annotations to an XFDF file
            pdfDoc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // Demonstrate importing the previously exported annotations into a new document
        using (Document importedDoc = new Document(pdfPath))
        {
            // Import annotations from the XFDF file
            importedDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the document with imported annotations
            importedDoc.Save(importedPdf);
        }

        Console.WriteLine("PDF created, annotations exported to XFDF, and imported back successfully.");
    }
}