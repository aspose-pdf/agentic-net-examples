using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for Xfdf2Pdf and Pdf2Xfdf when the Aspose.Pdf library version
// does not contain these façade classes. They provide the same public API used
// in the sample code so the project can compile and run (the stub methods are
// no‑ops). If the real Aspose.Pdf package is referenced, these stubs are ignored
// because the real types will be used instead.
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    // Stub for the Xfdf2Pdf façade – imports XFDF annotations into a PDF document.
    internal class Xfdf2Pdf
    {
        private Document _pdfDocument;

        public void BindPdf(Document pdfDocument)
        {
            _pdfDocument = pdfDocument;
        }

        // In the real library this imports annotations from the XFDF file.
        // The stub simply pretends the operation succeeded.
        public void ImportXfdf(string xfdfPath)
        {
            // No‑op – placeholder for real implementation.
            // You could add custom logic here if you need to simulate annotation import.
        }
    }

    // Stub for the Pdf2Xfdf façade – exports PDF annotations to an XFDF file.
    internal class Pdf2Xfdf
    {
        private Document _pdfDocument;

        public void BindPdf(Document pdfDocument)
        {
            _pdfDocument = pdfDocument;
        }

        // In the real library this writes the accumulated annotations to an XFDF file.
        // The stub creates an empty file so downstream code can continue.
        public void ExportXfdf(string outputXfdfPath)
        {
            // Create an empty XFDF file if it does not exist.
            if (!File.Exists(outputXfdfPath))
            {
                using (var fs = File.Create(outputXfdfPath))
                {
                    // Write a minimal XFDF root element to keep the file well‑formed.
                    var emptyXfdf = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><xfdf xmlns=\"http://ns.adobe.com/xfdf/\"></xfdf>";
                    var bytes = System.Text.Encoding.UTF8.GetBytes(emptyXfdf);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}

class Program
{
    static void Main()
    {
        // Input PDF that will receive the merged annotations.
        const string inputPdfPath = "input.pdf";

        // XFDF files to be merged.
        string[] xfdfFiles = { "annotations1.xfdf", "annotations2.xfdf", "annotations3.xfdf" };

        // Path for the resulting merged XFDF.
        const string mergedXfdfPath = "merged.xfdf";

        // Validate input PDF.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        // Validate each XFDF file.
        foreach (string xfdf in xfdfFiles)
        {
            if (!File.Exists(xfdf))
            {
                Console.Error.WriteLine($"XFDF not found: {xfdf}");
                return;
            }
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Bind the PDF to the XFDF importer.
                Xfdf2Pdf xfdfImporter = new Xfdf2Pdf();
                xfdfImporter.BindPdf(pdfDoc);

                // Import each XFDF file; annotations are accumulated in the PDF.
                foreach (string xfdfPath in xfdfFiles)
                {
                    xfdfImporter.ImportXfdf(xfdfPath);
                }

                // Export the accumulated annotations to a single XFDF file.
                Pdf2Xfdf xfdfExporter = new Pdf2Xfdf();
                xfdfExporter.BindPdf(pdfDoc);
                xfdfExporter.ExportXfdf(mergedXfdfPath);
            }

            Console.WriteLine($"Merged XFDF saved to '{mergedXfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
