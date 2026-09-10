using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

public static class AsyncAnnotationHelper
{
    // Flattens all annotations in the PDF asynchronously.
    public static async Task FlattenAnnotationsAsync(string inputPdf, string outputPdf)
    {
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            await Task.Run(() =>
            {
                editor.BindPdf(inputPdf);
                editor.FlatteningAnnotations();
                editor.Save(outputPdf);
            });
        }
    }

    // Deletes a specific annotation by its name asynchronously.
    public static async Task DeleteAnnotationAsync(string inputPdf, string annotationName, string outputPdf)
    {
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            await Task.Run(() =>
            {
                editor.BindPdf(inputPdf);
                editor.DeleteAnnotation(annotationName);
                editor.Save(outputPdf);
            });
        }
    }

    // Deletes all annotations in the PDF asynchronously.
    public static async Task DeleteAllAnnotationsAsync(string inputPdf, string outputPdf)
    {
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            await Task.Run(() =>
            {
                editor.BindPdf(inputPdf);
                editor.DeleteAnnotations();
                editor.Save(outputPdf);
            });
        }
    }

    // Imports annotations from an FDF file into the PDF asynchronously.
    public static async Task ImportAnnotationsFromFdfAsync(string inputPdf, string fdfFile, string outputPdf)
    {
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            await Task.Run(() =>
            {
                editor.BindPdf(inputPdf);
                editor.ImportAnnotationsFromFdf(fdfFile);
                editor.Save(outputPdf);
            });
        }
    }

    // Exports all annotations from the PDF to an XFDF file asynchronously.
    public static async Task ExportAnnotationsToXfdfAsync(string inputPdf, string xfdfFile)
    {
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            await Task.Run(() =>
            {
                editor.BindPdf(inputPdf);
                using (var stream = File.Create(xfdfFile))
                {
                    editor.ExportAnnotationsToXfdf(stream);
                }
            });
        }
    }

    // Flattens specific annotation types within a page range asynchronously.
    public static async Task FlattenSpecificAnnotationsAsync(string inputPdf, string outputPdf, int startPage, int endPage, AnnotationType[] types)
    {
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            await Task.Run(() =>
            {
                editor.BindPdf(inputPdf);
                editor.FlatteningAnnotations(startPage, endPage, types);
                editor.Save(outputPdf);
            });
        }
    }
}

public class Program
{
    private const string SamplePdfPath = "sample.pdf";
    private const string FlattenedPdfPath = "flattened.pdf";
    private const string DeletedPdfPath = "deleted.pdf";
    private const string ImportedPdfPath = "imported.pdf";
    private const string FdfFilePath = "annotations.fdf";
    private const string XfdfFilePath = "exported.xfdf";

    public static async Task Main()
    {
        // Ensure a sample PDF exists for the demo.
        EnsureSamplePdfExists(SamplePdfPath);

        // Flatten all annotations.
        await AsyncAnnotationHelper.FlattenAnnotationsAsync(SamplePdfPath, FlattenedPdfPath);

        // Delete a specific annotation by name.
        await AsyncAnnotationHelper.DeleteAnnotationAsync(SamplePdfPath, "Note1", DeletedPdfPath);

        // Import annotations from an FDF file (the FDF file is optional – the call will simply demonstrate the API).
        if (File.Exists(FdfFilePath))
        {
            await AsyncAnnotationHelper.ImportAnnotationsFromFdfAsync(SamplePdfPath, FdfFilePath, ImportedPdfPath);
        }

        // Export annotations to an XFDF file.
        await AsyncAnnotationHelper.ExportAnnotationsToXfdfAsync(SamplePdfPath, XfdfFilePath);

        // Flatten only Line and FreeText annotations on pages 1‑2.
        // The sample PDF now contains at least two pages, so the range is valid.
        AnnotationType[] types = new AnnotationType[] { AnnotationType.Line, AnnotationType.FreeText };
        await AsyncAnnotationHelper.FlattenSpecificAnnotationsAsync(SamplePdfPath, "flattened_specific.pdf", 1, 2, types);
    }

    // Creates a minimal PDF with two pages and a sample TextAnnotation named "Note1" on the first page.
    private static void EnsureSamplePdfExists(string path)
    {
        if (File.Exists(path))
            return;

        using (Document doc = new Document())
        {
            // First page – contains the annotation.
            Page page1 = doc.Pages.Add();
            var rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            var textAnn = new TextAnnotation(page1, rect)
            {
                Name = "Note1",
                Title = "Sample Note",
                Contents = "This is a sample annotation."
            };
            page1.Annotations.Add(textAnn);

            // Second page – empty, added to satisfy the page‑range used later.
            doc.Pages.Add();

            doc.Save(path);
        }
    }
}
