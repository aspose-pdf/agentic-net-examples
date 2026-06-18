using System;
using Aspose.Pdf.Facades;               // Facade classes (PdfAnnotationEditor, etc.)
using Aspose.Pdf.Annotations;           // AnnotationType enum

/// <summary>
/// Helper class for common annotation operations using Aspose.Pdf.Facades.
/// </summary>
public class PdfAnnotationHelper : IDisposable
{
    // Underlying facade instance.
    private readonly PdfAnnotationEditor _editor;

    /// <summary>
    /// Initializes a new instance of the helper.
    /// </summary>
    public PdfAnnotationHelper()
    {
        // Create the PdfAnnotationEditor (no document bound yet).
        _editor = new PdfAnnotationEditor();
    }

    /// <summary>
    /// Binds the helper to an existing PDF file.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF.</param>
    public void Bind(string pdfPath)
    {
        // Initialize the facade with the PDF file.
        _editor.BindPdf(pdfPath);
    }

    /// <summary>
    /// Deletes all annotations from the bound document.
    /// </summary>
    public void DeleteAllAnnotations()
    {
        _editor.DeleteAnnotations();
    }

    /// <summary>
    /// Deletes all annotations of a specific type.
    /// </summary>
    /// <param name="annotationType">Annotation type name (e.g., "Text", "Link").</param>
    public void DeleteAnnotationsByType(string annotationType)
    {
        _editor.DeleteAnnotations(annotationType);
    }

    /// <summary>
    /// Flattens all remaining annotations in the document.
    /// </summary>
    public void FlattenAllAnnotations()
    {
        // The correct method name in Aspose.Pdf.Facades is FlatteningAnnotations().
        _editor.FlatteningAnnotations();
    }

    /// <summary>
    /// Saves the modified PDF to the specified path.
    /// </summary>
    /// <param name="outputPath">Destination file path.</param>
    public void Save(string outputPath)
    {
        // Save writes a PDF regardless of the file extension.
        _editor.Save(outputPath);
    }

    /// <summary>
    /// Releases all resources used by the helper.
    /// </summary>
    public void Dispose()
    {
        _editor?.Dispose();
    }
}

// ---------------------------------------------------------------------------
// Dummy entry point to satisfy the compiler when the project is built as an
// executable. The helper itself is intended for library use; the Main method
// simply demonstrates a no‑op usage and can be removed or replaced when the
// project type is changed to a class library.
// ---------------------------------------------------------------------------
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – placeholder to provide a static Main entry point.
        // Example usage (commented out to avoid side‑effects during build):
        // using (var helper = new PdfAnnotationHelper())
        // {
        //     helper.Bind("input.pdf");
        //     helper.DeleteAllAnnotations();
        //     helper.FlattenAllAnnotations();
        //     helper.Save("output.pdf");
        // }
    }
}