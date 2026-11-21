using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class ExportAnnotationsTest
{
    static void Main()
    {
        // Paths for temporary PDF and XFDF files
        string pdfPath = Path.Combine(Path.GetTempPath(), "empty_annotations.pdf");
        string xfdfPath = Path.Combine(Path.GetTempPath(), "exported_annotations.xfdf");

        // Ensure any previous files are removed
        if (File.Exists(pdfPath)) File.Delete(pdfPath);
        if (File.Exists(xfdfPath)) File.Delete(xfdfPath);

        try
        {
            // Create a minimal PDF with a single blank page
            using (Document doc = new Document())
            {
                // Add an empty page (Pages are 1‑based)
                doc.Pages.Add();
                doc.Save(pdfPath);
            }

            // Export annotations (there are none) to XFDF using the correct API
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfPath);
                using (FileStream fs = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    // Export all annotations (null type array) for the whole document
                    editor.ExportAnnotationsXfdf(fs, 1, editor.Document.Pages.Count, (string[])null);
                }
            }

            // Verify that the XFDF file exists and contains a valid XFDF root element
            if (!File.Exists(xfdfPath))
            {
                Console.Error.WriteLine("XFDF file was not created.");
                return;
            }

            string xfdfContent = File.ReadAllText(xfdfPath);
            if (xfdfContent.Contains("<xfdf"))
            {
                Console.WriteLine("ExportAnnotations produced a valid XFDF file for a PDF with no annotations.");
            }
            else
            {
                Console.Error.WriteLine("XFDF content is invalid or empty.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during test execution: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files
            if (File.Exists(pdfPath)) File.Delete(pdfPath);
            if (File.Exists(xfdfPath)) File.Delete(xfdfPath);
        }
    }
}
