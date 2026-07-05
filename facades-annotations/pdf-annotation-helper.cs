using System;
using System.IO;
using Aspose.Pdf.Facades;               // PdfAnnotationEditor
using Aspose.Pdf.Annotations;           // AnnotationType

namespace PdfAnnotationUtilities
{
    /// <summary>
    /// Helper class that wraps Aspose.Pdf.Facades.PdfAnnotationEditor
    /// and provides common annotation operations: bind, delete, flatten, save.
    /// </summary>
    public sealed class PdfAnnotationHelper : IDisposable
    {
        // The underlying editor instance.
        private readonly Aspose.Pdf.Facades.PdfAnnotationEditor _editor;

        /// <summary>
        /// Initializes a new instance of the helper.
        /// </summary>
        public PdfAnnotationHelper()
        {
            // Use the parameterless constructor as defined in the API.
            _editor = new Aspose.Pdf.Facades.PdfAnnotationEditor();
        }

        /// <summary>
        /// Binds the helper to an existing PDF file.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF.</param>
        public void Bind(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            // BindPdf(string) is the official binding method.
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
        /// <param name="annotationTypeName">
        /// The annotation type name (e.g., "Text", "Highlight").
        /// </param>
        public void DeleteAnnotationsByType(string annotationTypeName)
        {
            if (string.IsNullOrWhiteSpace(annotationTypeName))
                throw new ArgumentException("Annotation type name must be provided.", nameof(annotationTypeName));

            _editor.DeleteAnnotations(annotationTypeName);
        }

        /// <summary>
        /// Flattens all annotations in the document (makes them part of the page content).
        /// </summary>
        public void FlattenAllAnnotations()
        {
            _editor.FlatteningAnnotations();
        }

        /// <summary>
        /// Flattens specific annotation types within a page range.
        /// </summary>
        /// <param name="startPage">1‑based start page number.</param>
        /// <param name="endPage">1‑based end page number.</param>
        /// <param name="types">Array of annotation types to flatten.</param>
        public void FlattenAnnotations(int startPage, int endPage, AnnotationType[] types)
        {
            if (types == null || types.Length == 0)
                throw new ArgumentException("At least one annotation type must be specified.", nameof(types));

            // The API overload expects start, end, and an array of AnnotationType.
            _editor.FlatteningAnnotations(startPage, endPage, types);
        }

        /// <summary>
        /// Saves the modified PDF to the specified output path.
        /// </summary>
        /// <param name="outputPath">Full path where the PDF will be saved.</param>
        public void Save(string outputPath)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // Save(string) writes a PDF regardless of the file extension.
            _editor.Save(outputPath);
        }

        /// <summary>
        /// Releases all resources used by the helper.
        /// </summary>
        public void Dispose()
        {
            // Close releases the bound document; Dispose frees the facade itself.
            _editor?.Close();
            _editor?.Dispose();
        }
    }

    // Example usage of the helper class.
    internal class Program
    {
        private static void Main()
        {
            const string inputPdf = "sample.pdf";
            const string outputPdf = "sample_cleaned.pdf";

            // Ensure the input file exists.
            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input file not found: {inputPdf}");
                return;
            }

            // Use the helper inside a using block to guarantee disposal.
            using (var helper = new PdfAnnotationHelper())
            {
                helper.Bind(inputPdf);

                // Delete all highlight annotations, keep others.
                helper.DeleteAnnotationsByType("Highlight");

                // Flatten all remaining annotations.
                helper.FlattenAllAnnotations();

                // Save the result.
                helper.Save(outputPdf);
            }

            Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
        }
    }
}