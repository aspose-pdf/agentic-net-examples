using System;
using System.IO;
using System.Drawing;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

/// <summary>
/// Simplified wrapper around Aspose.Pdf.Facades.PdfAnnotationEditor.
/// Provides common annotation operations with a clean API.
/// </summary>
public sealed class PdfAnnotationHelper : IDisposable
{
    private readonly PdfAnnotationEditor _editor;
    private bool _disposed;

    /// <summary>
    /// Initializes the helper and binds it to the specified PDF file.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF.</param>
    public PdfAnnotationHelper(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        _editor = new PdfAnnotationEditor();
        _editor.BindPdf(pdfPath);
    }

    /// <summary>
    /// Deletes a single annotation identified by its name.
    /// </summary>
    /// <param name="annotationName">The name of the annotation to delete.</param>
    public void DeleteAnnotation(string annotationName)
    {
        if (string.IsNullOrWhiteSpace(annotationName))
            throw new ArgumentException("Annotation name must be provided.", nameof(annotationName));

        _editor.DeleteAnnotation(annotationName);
    }

    /// <summary>
    /// Deletes all annotations in the document.
    /// </summary>
    public void DeleteAllAnnotations()
    {
        _editor.DeleteAnnotations();
    }

    /// <summary>
    /// Deletes all annotations of the specified types.
    /// </summary>
    /// <param name="types">Array of annotation types to remove.</param>
    public void DeleteAnnotationsByType(AnnotationType[] types)
    {
        if (types == null || types.Length == 0)
            throw new ArgumentException("At least one annotation type must be specified.", nameof(types));

        // PdfAnnotationEditor.DeleteAnnotations expects a comma‑separated string of type names.
        string typeList = string.Join(",", types.Select(t => t.ToString()));
        _editor.DeleteAnnotations(typeList);
    }

    /// <summary>
    /// Exports all annotations to an XFDF file.
    /// </summary>
    /// <param name="xfdfPath">Destination XFDF file path.</param>
    public void ExportAnnotations(string xfdfPath)
    {
        if (string.IsNullOrWhiteSpace(xfdfPath))
            throw new ArgumentException("XFDF path must be provided.", nameof(xfdfPath));

        using (FileStream fs = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
        {
            _editor.ExportAnnotationsToXfdf(fs);
        }
    }

    /// <summary>
    /// Imports annotations from an FDF file into the bound PDF.
    /// </summary>
    /// <param name="fdfPath">Source FDF file path.</param>
    public void ImportAnnotations(string fdfPath)
    {
        if (string.IsNullOrWhiteSpace(fdfPath))
            throw new ArgumentException("FDF path must be provided.", nameof(fdfPath));

        _editor.ImportAnnotationsFromFdf(fdfPath);
    }

    /// <summary>
    /// Modifies annotation properties for annotations within the given page range.
    /// </summary>
    /// <param name="startPage">First page (1‑based).</param>
    /// <param name="endPage">Last page (inclusive, 1‑based).</param>
    /// <param name="newAnnotation">Annotation instance containing the new property values.</param>
    public void ModifyAnnotations(int startPage, int endPage, Annotation newAnnotation)
    {
        if (newAnnotation == null)
            throw new ArgumentNullException(nameof(newAnnotation));

        if (startPage < 1 || endPage < startPage)
            throw new ArgumentException("Invalid page range.");

        _editor.ModifyAnnotations(startPage, endPage, newAnnotation);
    }

    /// <summary>
    /// Redacts a rectangular area on a specific page, filling it with the given color.
    /// </summary>
    /// <param name="pageNumber">Page number (1‑based).</param>
    /// <param name="rect">Rectangle defining the area to redact.</param>
    /// <param name="color">Fill color for the redacted area.</param>
    public void RedactArea(int pageNumber, Aspose.Pdf.Rectangle rect, System.Drawing.Color color)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be 1 or greater.", nameof(pageNumber));

        _editor.RedactArea(pageNumber, rect, color);
    }

    /// <summary>
    /// Saves the modified PDF to the specified output path.
    /// </summary>
    /// <param name="outputPath">Destination PDF file path.</param>
    public void Save(string outputPath)
    {
        if (string.IsNullOrWhiteSpace(outputPath))
            throw new ArgumentException("Output path must be provided.", nameof(outputPath));

        _editor.Save(outputPath);
    }

    /// <summary>
    /// Releases resources held by the underlying PdfAnnotationEditor.
    /// </summary>
    public void Close()
    {
        _editor.Close();
    }

    /// <summary>
    /// Implements IDisposable to ensure proper cleanup.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
            return;

        try
        {
            _editor?.Close();
        }
        catch
        {
            // Swallow any exception during close – Dispose should not throw.
        }

        if (_editor is IDisposable disposableEditor)
        {
            disposableEditor.Dispose();
        }

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}

/// <summary>
/// Minimal entry point to satisfy the compiler when the project is built as an executable.
/// The method does not perform any work; it simply demonstrates that the helper can be instantiated.
/// </summary>
public static class Program
{
    public static void Main(string[] args)
    {
        // No‑op entry point – required for projects that target an executable.
        // Example usage (commented out to avoid side effects during build):
        // using var helper = new PdfAnnotationHelper("sample.pdf");
        // helper.DeleteAllAnnotations();
        // helper.Save("output.pdf");
    }
}
