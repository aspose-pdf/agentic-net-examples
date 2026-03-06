using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdfPath   = "source_with_annotations.pdf"; // PDF that already contains annotations
        const string xslFoPath       = "template.xslfo";               // XSL‑FO file to be converted to PDF
        const string xfdfTempPath    = "temp_annotations.xfdf";        // Temporary XFDF file
        const string outputPdfPath   = "final_output.pdf";              // Resulting PDF with imported annotations

        // Verify input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Export annotations from the source PDF to an XFDF file.
            // ------------------------------------------------------------
            using (Document srcDoc = new Document(sourcePdfPath))
            {
                srcDoc.ExportAnnotationsToXfdf(xfdfTempPath);
            }

            // ------------------------------------------------------------
            // 2. Load the XSL‑FO file and create a PDF document from it.
            // ------------------------------------------------------------
            XslFoLoadOptions xslFoOptions = new XslFoLoadOptions(xslFoPath);
            using (Document pdfFromFo = new Document(xslFoPath, xslFoOptions))
            {
                // ------------------------------------------------------------
                // 3. Import the previously exported annotations into the new PDF.
                // ------------------------------------------------------------
                pdfFromFo.ImportAnnotationsFromXfdf(xfdfTempPath);

                // ------------------------------------------------------------
                // 4. Save the final PDF.
                // ------------------------------------------------------------
                pdfFromFo.Save(outputPdfPath);
            }

            // Clean up temporary XFDF file
            if (File.Exists(xfdfTempPath))
                File.Delete(xfdfTempPath);

            Console.WriteLine($"PDF created with imported annotations: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}