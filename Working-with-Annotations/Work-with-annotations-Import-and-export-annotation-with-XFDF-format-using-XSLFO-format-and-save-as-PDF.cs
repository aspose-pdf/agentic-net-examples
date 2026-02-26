using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xslfoPath      = "input.xslfo";
        const string xfdfPath       = "annotations.xfdf";
        const string outputPdfPath  = "output.pdf";

        // Verify source XSL‑FO file exists
        if (!File.Exists(xslfoPath))
        {
            Console.Error.WriteLine($"Source XSL‑FO file not found: {xslfoPath}");
            return;
        }

        // Load the XSL‑FO document and convert it to a PDF in memory
        var loadOptions = new Aspose.Pdf.XslFoLoadOptions();
        using (Document pdfDoc = new Document(xslfoPath, loadOptions))
        {
            // -----------------------------------------------------------------
            // Export existing annotations (if any) to XFDF using PdfAnnotationEditor
            // -----------------------------------------------------------------
            var editor = new Aspose.Pdf.Facades.PdfAnnotationEditor();
            editor.BindPdf(pdfDoc);

            using (FileStream xfdfExport = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                editor.ExportAnnotationsToXfdf(xfdfExport);
            }

            // -----------------------------------------------------------------
            // Import annotations back from the XFDF file (demonstrates round‑trip)
            // -----------------------------------------------------------------
            using (FileStream xfdfImport = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                editor.ImportAnnotationsFromXfdf(xfdfImport);
            }

            // Save the final PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with imported annotations saved to '{outputPdfPath}'.");
    }
}