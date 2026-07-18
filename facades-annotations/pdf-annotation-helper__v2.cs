using System;
using System.Drawing; // Required for Rectangle used by PdfContentEditor
using Aspose.Pdf.Facades; // Facade classes for annotation handling
using Aspose.Pdf.Annotations; // Annotation base class

namespace PdfAnnotationUtilities
{
    /// <summary>
    /// Helper class that abstracts common annotation operations using Aspose.Pdf.Facades.
    /// It internally manages both PdfAnnotationEditor (for modify/delete/flatten) and
    /// PdfContentEditor (for creating new annotations).
    /// </summary>
    public sealed class PdfAnnotationHelper : IDisposable
    {
        private readonly PdfAnnotationEditor _annotationEditor;
        private readonly PdfContentEditor _contentEditor;
        private bool _disposed;

        /// <summary>
        /// Initializes the helper and binds it to an existing PDF file.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF document.</param>
        public PdfAnnotationHelper(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            // Bind the PDF to both editors.
            _annotationEditor = new PdfAnnotationEditor();
            _annotationEditor.BindPdf(pdfPath);

            _contentEditor = new PdfContentEditor();
            _contentEditor.BindPdf(pdfPath);
        }

        /// <summary>
        /// Adds a text annotation (sticky note) to the specified page.
        /// </summary>
        /// <param name="page">1‑based page number where the annotation will be placed.</param>
        /// <param name="rect">Rectangle defining the annotation bounds.</param>
        /// <param name="title">Title of the annotation (appears in the popup).</param>
        /// <param name="contents">Main text of the annotation.</param>
        /// <param name="open">If true, the annotation is displayed open initially.</param>
        /// <param name="icon">Icon name (e.g., "Comment", "Key", "Note").</param>
        public void AddTextAnnotation(int page, Rectangle rect, string title, string contents, bool open, string icon)
        {
            if (page < 1) throw new ArgumentOutOfRangeException(nameof(page), "Page numbers are 1‑based.");
            // PdfContentEditor.CreateText expects the page number as an int (per Aspose API).
            _contentEditor.CreateText(rect, title, contents, open, icon, page);
        }

        /// <summary>
        /// Deletes a single annotation identified by its name.
        /// </summary>
        /// <param name="annotationName">The Name property of the annotation to delete.</param>
        public void DeleteAnnotation(string annotationName)
        {
            if (string.IsNullOrWhiteSpace(annotationName))
                throw new ArgumentException("Annotation name must be provided.", nameof(annotationName));

            _annotationEditor.DeleteAnnotation(annotationName);
        }

        /// <summary>
        /// Deletes all annotations in the document.
        /// </summary>
        public void DeleteAllAnnotations()
        {
            _annotationEditor.DeleteAnnotations();
        }

        /// <summary>
        /// Deletes all annotations on a specific page.
        /// </summary>
        /// <param name="page">1‑based page number.</param>
        public void DeleteAnnotationsOnPage(int page)
        {
            if (page < 1) throw new ArgumentOutOfRangeException(nameof(page), "Page numbers are 1‑based.");
            // PdfAnnotationEditor.DeleteAnnotations expects the page number as a string.
            _annotationEditor.DeleteAnnotations(page.ToString());
        }

        /// <summary>
        /// Saves the modified PDF to the specified path.
        /// </summary>
        /// <param name="outputPath">File path where the PDF should be saved.</param>
        public void Save(string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            _annotationEditor.Save(outputPath);
        }

        /// <summary>
        /// Implements the Dispose pattern to release Aspose resources.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _annotationEditor?.Dispose();
                _contentEditor?.Dispose();
                _disposed = true;
            }
        }
    }

    /// <summary>
    /// Simple console entry point that demonstrates the usage of PdfAnnotationHelper.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Expected arguments: <inputPdf> <outputPdf>
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PdfAnnotationUtilities <input-pdf> <output-pdf>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Example: add a sticky note on page 1.
            using (var helper = new PdfAnnotationHelper(inputPath))
            {
                var rect = new Rectangle(100, 100, 200, 200); // x, y, width, height
                helper.AddTextAnnotation(
                    page: 1,
                    rect: rect,
                    title: "Note",
                    contents: "This is a sample annotation added via PdfAnnotationHelper.",
                    open: true,
                    icon: "Comment");

                // Save the modified document.
                helper.Save(outputPath);
            }

            Console.WriteLine($"Annotation added and PDF saved to '{outputPath}'.");
        }
    }
}
