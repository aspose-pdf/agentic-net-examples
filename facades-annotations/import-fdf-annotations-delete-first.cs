using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";        // source PDF
        const string fdfPath    = "annotations.fdf"; // FDF containing annotations
        const string outputPath = "output.pdf";      // result PDF

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor implements IDisposable – use using for deterministic disposal
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the existing PDF document
                editor.BindPdf(pdfPath);

                // Import annotations from the FDF file into the bound PDF
                // Correct method name is ImportAnnotationsFromFdf (not ImportFdf)
                editor.ImportAnnotationsFromFdf(fdfPath);

                // Retrieve all annotations on page 1
                // ExtractAnnotations returns IList<Annotation>. Passing null for types gets all types.
                var pageAnnotations = editor.ExtractAnnotations(1, 1, (AnnotationType[])null);
                if (pageAnnotations != null && pageAnnotations.Count > 0)
                {
                    // Delete the first annotation – DeleteAnnotation expects the annotation's Name.
                    var firstAnnotation = pageAnnotations[0];
                    if (!string.IsNullOrEmpty(firstAnnotation.Name))
                    {
                        editor.DeleteAnnotation(firstAnnotation.Name);
                    }
                }

                // Save the modified PDF to the output path
                editor.Save(outputPath);
            }

            Console.WriteLine($"Annotations imported, first annotation removed, saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
