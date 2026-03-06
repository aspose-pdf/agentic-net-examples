using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, XFDF file containing extra annotations, and the final XPS output.
        const string sourcePdfPath = "input.pdf";
        const string xfdfPath = "extra_annotations.xfdf";
        const string tempPdfPath = "temp_with_annotations.pdf";
        const string outputXpsPath = "output.xps";

        // Ensure the source files exist.
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Load the PDF and import extra annotations from the XFDF.
        // -----------------------------------------------------------------
        // PdfAnnotationEditor works with PDF annotations. It can bind to a PDF
        // file, import annotations from an XFDF file, and then save the modified
        // PDF back to disk.
        using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
        {
            // Bind the editor to the existing PDF document.
            annotationEditor.BindPdf(sourcePdfPath);

            // Import all annotations from the XFDF file into the bound PDF.
            annotationEditor.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the PDF that now contains the extra annotations.
            annotationEditor.Save(tempPdfPath);
        }

        // ---------------------------------------------------------------
        // Step 2: Convert the annotated PDF to XPS format using XpsSaveOptions.
        // ---------------------------------------------------------------
        // The Document class is used for conversion. It must be disposed via
        // a using block to avoid file‑handle leaks.
        using (Document pdfDocument = new Document(tempPdfPath))
        {
            // Initialize XPS save options (default constructor is sufficient for most cases).
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // Save the document as XPS.
            pdfDocument.Save(outputXpsPath, xpsOptions);
        }

        // Optional cleanup: delete the temporary PDF file.
        try
        {
            if (File.Exists(tempPdfPath))
                File.Delete(tempPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"Conversion completed. XPS file saved to '{outputXpsPath}'.");
    }
}