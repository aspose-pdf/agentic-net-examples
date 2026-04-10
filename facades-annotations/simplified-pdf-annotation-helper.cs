using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

/// <summary>
/// Helper class that wraps Aspose.Pdf.Facades.PdfAnnotationEditor and provides
/// simplified methods for common annotation operations such as adding, deleting,
/// modifying, importing, exporting and saving annotations.
/// </summary>
public class PdfAnnotationHelper : IDisposable
{
    private readonly PdfAnnotationEditor _annotationEditor;
    private readonly PdfContentEditor _contentEditor;
    private bool _disposed;

    /// <summary>
    /// Initializes the helper and binds it to an existing PDF file.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF document to work with.</param>
    public PdfAnnotationHelper(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be a non‑empty string.", nameof(pdfPath));

        // Bind both facades to the same document.
        _annotationEditor = new PdfAnnotationEditor();
        _annotationEditor.BindPdf(pdfPath);

        _contentEditor = new PdfContentEditor();
        _contentEditor.BindPdf(pdfPath);
    }

    /// <summary>
    /// Adds a text (comment) annotation on the specified page.
    /// </summary>
    /// <param name="pageNumber">1‑based page index.</param>
    /// <param name="rect">Aspose.Pdf.Rectangle that defines the annotation location.</param>
    /// <param name="title">Title of the annotation.</param>
    /// <param name="contents">Annotation contents.</param>
    /// <param name="open">If true the annotation is opened by default.</param>
    /// <param name="icon">Icon name (e.g., "Comment", "Key", "Note", "Help", "NewParagraph", "Paragraph", "Insert").</param>
    public void AddTextAnnotation(int pageNumber, Aspose.Pdf.Rectangle rect, string title, string contents, bool open = true, string icon = "Note")
    {
        // Convert Aspose.Pdf.Rectangle to System.Drawing.Rectangle because PdfContentEditor expects the latter.
        var sysRect = new System.Drawing.Rectangle(
            (int)rect.LLX,
            (int)rect.LLY,
            (int)(rect.URX - rect.LLX),
            (int)(rect.URY - rect.LLY));

        // PdfContentEditor.CreateText creates a text annotation.
        _contentEditor.CreateText(sysRect, title, contents, open, icon, pageNumber);
    }

    /// <summary>
    /// Adds a markup annotation (highlight, underline, strikeout, squiggly) on the specified page.
    /// </summary>
    /// <param name="pageNumber">1‑based page index.</param>
    /// <param name="rect">Aspose.Pdf.Rectangle that defines the annotation location.</param>
    /// <param name="contents">Optional contents displayed when the annotation is selected.</param>
    /// <param name="type">
    /// 0 = Highlight, 1 = Underline, 2 = StrikeOut, 3 = Squiggly.
    /// </param>
    /// <param name="color">System.Drawing.Color of the markup.</param>
    public void AddMarkupAnnotation(int pageNumber, Aspose.Pdf.Rectangle rect, string contents, int type, System.Drawing.Color color)
    {
        // Convert rectangle
        var sysRect = new System.Drawing.Rectangle(
            (int)rect.LLX,
            (int)rect.LLY,
            (int)(rect.URX - rect.LLX),
            (int)(rect.URY - rect.LLY));

        // PdfContentEditor.CreateMarkup creates the markup annotation.
        _contentEditor.CreateMarkup(sysRect, contents, type, pageNumber, color);
    }

    /// <summary>
    /// Deletes all annotations from the document.
    /// </summary>
    public void DeleteAllAnnotations()
    {
        _annotationEditor.DeleteAnnotations();
    }

    /// <summary>
    /// Deletes all annotations of a specific type (e.g., "Text", "Link").
    /// </summary>
    /// <param name="annotationTypeName">Name of the annotation type to delete.</param>
    public void DeleteAnnotationsByType(string annotationTypeName)
    {
        if (string.IsNullOrWhiteSpace(annotationTypeName))
            throw new ArgumentException("Annotation type name must be provided.", nameof(annotationTypeName));

        _annotationEditor.DeleteAnnotations(annotationTypeName);
    }

    /// <summary>
    /// Exports all annotations to an XFDF file.
    /// </summary>
    /// <param name="xfdfPath">Destination XFDF file path.</param>
    public void ExportAnnotations(string xfdfPath)
    {
        if (string.IsNullOrWhiteSpace(xfdfPath))
            throw new ArgumentException("XFDF path must be provided.", nameof(xfdfPath));

        // Ensure the directory exists.
        string dir = Path.GetDirectoryName(xfdfPath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        using (FileStream fs = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
        {
            _annotationEditor.ExportAnnotationsToXfdf(fs);
        }
    }

    /// <summary>
    /// Imports annotations from an XFDF file into the current document.
    /// </summary>
    /// <param name="xfdfPath">Source XFDF file path.</param>
    public void ImportAnnotationsFromXfdf(string xfdfPath)
    {
        if (string.IsNullOrWhiteSpace(xfdfPath))
            throw new ArgumentException("XFDF path must be provided.", nameof(xfdfPath));

        _annotationEditor.ImportAnnotationsFromXfdf(xfdfPath);
    }

    /// <summary>
    /// Imports annotations from an FDF file into the current document.
    /// </summary>
    /// <param name="fdfPath">Source FDF file path.</param>
    public void ImportAnnotationsFromFdf(string fdfPath)
    {
        if (string.IsNullOrWhiteSpace(fdfPath))
            throw new ArgumentException("FDF path must be provided.", nameof(fdfPath));

        _annotationEditor.ImportAnnotationsFromFdf(fdfPath);
    }

    /// <summary>
    /// Modifies annotation properties (title, contents, color, etc.) for annotations
    /// within the specified page range.
    /// </summary>
    /// <param name="startPage">1‑based start page.</param>
    /// <param name="endPage">1‑based end page.</param>
    /// <param name="newAnnotation">Annotation instance containing the new property values.</param>
    public void ModifyAnnotations(int startPage, int endPage, Annotation newAnnotation)
    {
        if (newAnnotation == null)
            throw new ArgumentNullException(nameof(newAnnotation));

        _annotationEditor.ModifyAnnotations(startPage, endPage, newAnnotation);
    }

    /// <summary>
    /// Flattens all annotations, converting them into regular page content.
    /// </summary>
    public void FlattenAllAnnotations()
    {
        _annotationEditor.FlatteningAnnotations();
    }

    /// <summary>
    /// Saves the modified PDF to the specified output path.
    /// </summary>
    /// <param name="outputPath">Destination PDF file path.</param>
    public void Save(string outputPath)
    {
        if (string.IsNullOrWhiteSpace(outputPath))
            throw new ArgumentException("Output path must be provided.", nameof(outputPath));

        // Ensure the directory exists.
        string dir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        _annotationEditor.Save(outputPath);
    }

    /// <summary>
    /// Releases resources used by the helper.
    /// </summary>
    public void Dispose()
    {
        if (_disposed) return;

        // Close facades; they release the underlying Document.
        _annotationEditor.Close();
        _contentEditor.Close();

        _disposed = true;
    }
}

// ---------------------------------------------------------------------------
// Minimal entry point to satisfy the compiler when the project is built as an
// executable. The helper class is intended for library use; the Main method does
// nothing but allows the project to compile without requiring external changes.
// ---------------------------------------------------------------------------
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library can be used by referencing the assembly.
    }
}
