using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input files
        const string inputXpsPath   = "sample.xps";      // XPS source document
        const string inputXfdfPath  = "annotations.xfdf"; // XFDF file containing annotations to import
        // Output files
        const string exportedXfdfPath = "exported_annotations.xfdf"; // XFDF file where current annotations will be exported
        const string outputPdfPath    = "result.pdf";                // Final PDF with imported annotations

        // Verify input files exist
        if (!File.Exists(inputXpsPath))
        {
            Console.Error.WriteLine($"XPS file not found: {inputXpsPath}");
            return;
        }
        if (!File.Exists(inputXfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {inputXfdfPath}");
            return;
        }

        try
        {
            // Load the XPS document and convert it to a PDF Document object.
            // XpsLoadOptions is in the Aspose.Pdf namespace.
            using (Document pdfDoc = new Document(inputXpsPath, new XpsLoadOptions()))
            {
                // Create a PdfAnnotationEditor and bind it to the PDF document.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfDoc);

                    // ------------------------------------------------------------
                    // Export any existing annotations in the PDF to an XFDF file.
                    // (If the PDF has no annotations the file will be empty.)
                    // ------------------------------------------------------------
                    using (FileStream exportStream = File.Create(exportedXfdfPath))
                    {
                        editor.ExportAnnotationsToXfdf(exportStream);
                    }

                    // ------------------------------------------------------------
                    // Import annotations from the provided XFDF file into the PDF.
                    // All annotation types are imported; you can filter by passing
                    // an AnnotationType[] overload if needed.
                    // ------------------------------------------------------------
                    using (FileStream importStream = File.OpenRead(inputXfdfPath))
                    {
                        editor.ImportAnnotationsFromXfdf(importStream);
                    }

                    // Save the modified PDF (with imported annotations) to disk.
                    editor.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"PDF created at '{outputPdfPath}'.");
            Console.WriteLine($"Annotations exported to '{exportedXfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}