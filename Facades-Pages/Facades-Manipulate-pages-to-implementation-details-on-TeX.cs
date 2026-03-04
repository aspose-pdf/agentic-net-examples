using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory that contains the source TeX file.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input TeX file and temporary / final PDF paths.
        string texFile       = Path.Combine(dataDir, "sample.tex");
        string tempPdfFile   = Path.Combine(dataDir, "intermediate.pdf");
        string finalPdfFile  = Path.Combine(dataDir, "final.pdf");

        // Verify the TeX source exists.
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX file not found: {texFile}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the TeX file and convert it to a PDF document.
        // -----------------------------------------------------------------
        TeXLoadOptions texLoadOptions = new TeXLoadOptions(); // default options
        using (Document pdfDoc = new Document(texFile, texLoadOptions))
        {
            // Save the intermediate PDF (required because PdfPageEditor works with a file path).
            pdfDoc.Save(tempPdfFile);
        }

        // -----------------------------------------------------------------
        // 2. Manipulate the PDF pages using the PdfPageEditor facade.
        // -----------------------------------------------------------------
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the intermediate PDF file.
            pageEditor.BindPdf(tempPdfFile);

            // Example manipulations:
            pageEditor.Rotation = 90;                     // Rotate all pages 90 degrees.
            pageEditor.Zoom = 0.8f;                       // Scale pages to 80% of original size.
            pageEditor.PageSize = Aspose.Pdf.PageSize.A4; // Change page size to A4.

            // Apply the changes to the document.
            pageEditor.ApplyChanges();

            // Save the resulting PDF.
            pageEditor.Save(finalPdfFile);
        }

        Console.WriteLine($"Processed PDF saved to: {finalPdfFile}");
    }
}