using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source XSL‑FO file, the XFDF file containing extra annotations,
        // and the final PDF output.
        const string xslFoPath   = "input.xslfo";
        const string xfdfPath    = "extra_annotations.xfdf";
        const string outputPdf   = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Load the XSL‑FO document and convert it to a PDF in memory.
        XslFoLoadOptions loadOptions = new XslFoLoadOptions(); // no external XSL needed
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Bind the generated PDF to the PdfAnnotationEditor facade.
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
            {
                annotEditor.BindPdf(pdfDoc);

                // Import the extra annotations from the XFDF file.
                // This adds the annotations to the PDF currently held by the editor.
                annotEditor.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the final PDF with the added annotations.
                annotEditor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with extra annotations saved to '{outputPdf}'.");
    }
}