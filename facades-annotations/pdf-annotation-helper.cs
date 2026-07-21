using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

/// <summary>
/// Helper class that encapsulates common annotation operations using Aspose.Pdf.Facades.PdfAnnotationEditor.
/// </summary>
public class PdfAnnotationHelper : IDisposable
{
    private readonly PdfAnnotationEditor _editor;
    private bool _isBound;

    /// <summary>
    /// Initializes a new instance of the helper.
    /// </summary>
    public PdfAnnotationHelper()
    {
        // Create the facade – uses the provided constructor.
        _editor = new PdfAnnotationEditor();
    }

    /// <summary>
    /// Binds the helper to a PDF file on disk.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF.</param>
    public void Bind(string pdfPath)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        // Bind the PDF file – uses the facade BindPdf method.
        _editor.BindPdf(pdfPath);
        _isBound = true;
    }

    /// <summary>
    /// Binds the helper to a PDF stream.
    /// </summary>
    /// <param name="pdfStream">Stream containing the PDF.</param>
    public void Bind(Stream pdfStream)
    {
        if (pdfStream == null)
            throw new ArgumentNullException(nameof(pdfStream));

        _editor.BindPdf(pdfStream);
        _isBound = true;
    }

    /// <summary>
    /// Deletes all annotations in the bound document.
    /// </summary>
    public void DeleteAllAnnotations()
    {
        EnsureBound();
        _editor.DeleteAnnotations(); // Deletes every annotation.
    }

    /// <summary>
    /// Deletes annotations of a specific type (e.g., "Text", "Link").
    /// </summary>
    /// <param name="annotationType">The annotation type name.</param>
    public void DeleteAnnotationsByType(string annotationType)
    {
        EnsureBound();
        if (string.IsNullOrEmpty(annotationType))
            throw new ArgumentException("Annotation type must be provided.", nameof(annotationType));

        _editor.DeleteAnnotations(annotationType); // Deletes only the specified type.
    }

    /// <summary>
    /// Flattens all annotations in the document.
    /// </summary>
    public void FlattenAllAnnotations()
    {
        EnsureBound();
        // Correct method name is FlatteningAnnotations (no parameters).
        _editor.FlatteningAnnotations();
    }

    /// <summary>
    /// Flattens annotations of specific types within a page range.
    /// </summary>
    /// <param name="startPage">1‑based start page.</param>
    /// <param name="endPage">1‑based end page.</param>
    /// <param name="types">Array of AnnotationType to flatten.</param>
    public void FlattenAnnotations(int startPage, int endPage, AnnotationType[] types)
    {
        EnsureBound();
        if (types == null)
            throw new ArgumentNullException(nameof(types));

        // Correct overload is FlatteningAnnotations(startPage, endPage, types).
        _editor.FlatteningAnnotations(startPage, endPage, types);
    }

    /// <summary>
    /// Saves the modified PDF to the specified output path.
    /// </summary>
    /// <param name="outputPath">Destination file path.</param>
    public void Save(string outputPath)
    {
        EnsureBound();
        if (string.IsNullOrEmpty(outputPath))
            throw new ArgumentException("Output path must be provided.", nameof(outputPath));

        // Save uses the facade's Save method – no extra SaveOptions needed for PDF output.
        _editor.Save(outputPath);
    }

    /// <summary>
    /// Ensures that a PDF has been bound before performing operations.
    /// </summary>
    private void EnsureBound()
    {
        if (!_isBound)
            throw new InvalidOperationException("PdfAnnotationHelper is not bound to a PDF document.");
    }

    /// <summary>
    /// Releases resources used by the facade.
    /// </summary>
    public void Dispose()
    {
        // Close releases the bound document; Dispose frees the facade itself.
        _editor?.Close();
        _editor?.Dispose();
    }
}

/// <summary>
/// Minimal entry point required for a console‑application project.
/// The method does not perform any work; it simply demonstrates that the
/// assembly now builds successfully.
/// </summary>
public static class Program
{
    public static void Main(string[] args)
    {
        // Example usage (optional). Commented out to keep the demo side‑effect free.
        // using (var helper = new PdfAnnotationHelper())
        // {
        //     helper.Bind("input.pdf");
        //     helper.DeleteAllAnnotations();
        //     helper.FlattenAllAnnotations();
        //     helper.Save("output.pdf");
        // }
    }
}

/* Example usage (outside of Main):
using (PdfAnnotationHelper helper = new PdfAnnotationHelper())
{
    helper.Bind("input.pdf");
    helper.DeleteAllAnnotations();                     // Remove everything
    helper.FlattenAllAnnotations();                    // Or flatten specific types
    helper.Save("output.pdf");
}
*/