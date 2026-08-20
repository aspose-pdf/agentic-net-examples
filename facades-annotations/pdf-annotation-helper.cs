using System;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace PdfUtilities
{
    /// <summary>
    /// Helper class for common annotation operations using Aspose.Pdf.Facades.PdfAnnotationEditor.
    /// </summary>
    public sealed class PdfAnnotationHelper : IDisposable
    {
        private readonly PdfAnnotationEditor _editor;
        private bool _isBound;

        /// <summary>
        /// Initializes a new instance of the helper.
        /// </summary>
        public PdfAnnotationHelper()
        {
            // Create the facade; it implements IDisposable.
            _editor = new PdfAnnotationEditor();
        }

        /// <summary>
        /// Binds the helper to an existing PDF file.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF.</param>
        public void Bind(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be a non‑empty string.", nameof(pdfPath));

            _editor.BindPdf(pdfPath);
            _isBound = true;
        }

        /// <summary>
        /// Deletes all annotations in the bound document.
        /// </summary>
        public void DeleteAllAnnotations()
        {
            EnsureBound();
            _editor.DeleteAnnotations();
        }

        /// <summary>
        /// Deletes all annotations of a specific type (e.g., "Text", "Link").
        /// </summary>
        /// <param name="annotationType">Annotation type name as defined by Aspose.Pdf.</param>
        public void DeleteAnnotationsByType(string annotationType)
        {
            EnsureBound();
            if (string.IsNullOrWhiteSpace(annotationType))
                throw new ArgumentException("Annotation type must be a non‑empty string.", nameof(annotationType));

            _editor.DeleteAnnotations(annotationType);
        }

        /// <summary>
        /// Flattens all annotations in the document.
        /// </summary>
        public void FlattenAllAnnotations()
        {
            EnsureBound();
            _editor.FlatteningAnnotations();
        }

        /// <summary>
        /// Flattens annotations of specified types within a page range.
        /// </summary>
        /// <param name="startPage">1‑based start page index.</param>
        /// <param name="endPage">1‑based end page index.</param>
        /// <param name="types">Array of annotation types to flatten.</param>
        public void FlattenAnnotations(int startPage, int endPage, Aspose.Pdf.Annotations.AnnotationType[] types)
        {
            EnsureBound();
            if (types == null || types.Length == 0)
                throw new ArgumentException("At least one annotation type must be provided.", nameof(types));

            // Aspose.Pdf uses 1‑based page indexing.
            if (startPage < 1 || endPage < startPage)
                throw new ArgumentOutOfRangeException(nameof(startPage), "Invalid page range.");

            _editor.FlatteningAnnotations(startPage, endPage, types);
        }

        /// <summary>
        /// Saves the modified PDF to the specified output path.
        /// </summary>
        /// <param name="outputPath">Path where the PDF will be saved.</param>
        public void Save(string outputPath)
        {
            EnsureBound();
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be a non‑empty string.", nameof(outputPath));

            // PdfAnnotationEditor inherits SaveableFacade, so Save(string) writes a PDF.
            _editor.Save(outputPath);
        }

        /// <summary>
        /// Ensures that a PDF has been bound before performing operations.
        /// </summary>
        private void EnsureBound()
        {
            if (!_isBound)
                throw new InvalidOperationException("No PDF is bound. Call Bind() first.");
        }

        /// <summary>
        /// Disposes the underlying PdfAnnotationEditor.
        /// </summary>
        public void Dispose()
        {
            _editor?.Dispose();
        }
    }

    /// <summary>
    /// Minimal console entry point required for a C# executable project.
    /// The library itself does not need to run any code; this stub satisfies the compiler.
    /// </summary>
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Example usage (commented out to keep the stub side‑effect free):
            // using var helper = new PdfAnnotationHelper();
            // helper.Bind("input.pdf");
            // helper.DeleteAllAnnotations();
            // helper.Save("output.pdf");
        }
    }
}
