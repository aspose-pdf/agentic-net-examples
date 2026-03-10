using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that contains annotations
        const string inputPdfPath = "input.pdf";

        // Temporary files for XFDF and XPS intermediate formats
        const string xfdfPath = "annotations.xfdf";
        const string xpsPath = "intermediate.xps";

        // Output PDF after the whole process
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load the source PDF and export its annotations to XFDF
            // -------------------------------------------------
            using (Document srcDoc = new Document(inputPdfPath))
            {
                // Export all annotations to an XFDF file
                srcDoc.ExportAnnotationsToXfdf(xfdfPath);
            }

            // -------------------------------------------------
            // 2. Convert the PDF to XPS format
            // -------------------------------------------------
            using (Document srcDoc = new Document(inputPdfPath))
            {
                // XPS save options (default constructor is sufficient)
                Aspose.Pdf.XpsSaveOptions xpsSaveOptions = new Aspose.Pdf.XpsSaveOptions();
                srcDoc.Save(xpsPath, xpsSaveOptions);
            }

            // -------------------------------------------------
            // 3. Load the XPS file back as a PDF document
            // -------------------------------------------------
            using (Document pdfFromXps = new Document(xpsPath, new Aspose.Pdf.XpsLoadOptions()))
            {
                // -------------------------------------------------
                // 4. Import the previously exported XFDF annotations
                // -------------------------------------------------
                PdfAnnotationEditor editor = new PdfAnnotationEditor();
                editor.BindPdf(pdfFromXps);
                editor.ImportAnnotationsFromXfdf(xfdfPath);
                editor.Save(outputPdfPath);
                editor.Close(); // Dispose the editor resources
            }

            Console.WriteLine($"Process completed. Output PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files (optional)
            try { if (File.Exists(xfdfPath)) File.Delete(xfdfPath); } catch { }
            try { if (File.Exists(xpsPath)) File.Delete(xpsPath); } catch { }
        }
    }
}