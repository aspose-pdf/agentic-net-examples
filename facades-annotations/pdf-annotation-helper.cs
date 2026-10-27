using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    /// <summary>
    /// Helper class that encapsulates common annotation operations using Aspose.Pdf.Facades.
    /// Supports binding a PDF, deleting all annotations, flattening annotations, and saving the result.
    /// </summary>
    public class PdfAnnotationHelper : IDisposable
    {
        private readonly PdfAnnotationEditor _editor;
        private bool _isBound;

        public PdfAnnotationHelper()
        {
            // PdfAnnotationEditor does not implement IDisposable, but we provide a Dispose pattern
            // in case future resources need cleanup.
            _editor = new PdfAnnotationEditor();
            _isBound = false;
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
                throw new FileNotFoundException("Source PDF not found.", pdfPath);

            _editor.BindPdf(pdfPath);
            _isBound = true;
        }

        /// <summary>
        /// Deletes all annotations from the bound PDF.
        /// </summary>
        public void DeleteAllAnnotations()
        {
            EnsureBound();
            // DeleteAnnotations removes every annotation present in the document.
            _editor.DeleteAnnotations();
        }

        /// <summary>
        /// Flattens all annotations, converting them into regular page content.
        /// </summary>
        public void FlattenAllAnnotations()
        {
            EnsureBound();
            // Correct method name is FlatteningAnnotations (not FlattenAnnotations).
            _editor.FlatteningAnnotations();
        }

        /// <summary>
        /// Saves the modified PDF to the specified output path.
        /// </summary>
        /// <param name="outputPath">Full path where the PDF will be saved.</param>
        public void Save(string outputPath)
        {
            EnsureBound();

            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // Ensure the directory exists.
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            _editor.Save(outputPath);
        }

        /// <summary>
        /// Convenience method that performs bind, delete, flatten, and save in one call.
        /// </summary>
        /// <param name="inputPdf">Path to the source PDF.</param>
        /// <param name="outputPdf">Path where the processed PDF will be saved.</param>
        /// <param name="deleteAnnotations">If true, all annotations will be removed.</param>
        /// <param name="flattenAnnotations">If true, annotations will be flattened.</param>
        public void Process(string inputPdf, string outputPdf, bool deleteAnnotations = false, bool flattenAnnotations = false)
        {
            Bind(inputPdf);

            if (deleteAnnotations)
                DeleteAllAnnotations();

            if (flattenAnnotations)
                FlattenAllAnnotations();

            Save(outputPdf);
        }

        private void EnsureBound()
        {
            if (!_isBound)
                throw new InvalidOperationException("No PDF has been bound. Call Bind() before performing operations.");
        }

        // Implement IDisposable to follow best practices, even though the underlying editor has no unmanaged resources.
        public void Dispose()
        {
            // No unmanaged resources to release, but reset state to prevent further use.
            _isBound = false;
        }
    }

    // A minimal entry point so the project compiles as an executable.
    // In real usage the helper would be referenced from another project or from a test harness.
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Example usage (no real files are required for compilation).
            // Users of the library should replace the paths with actual PDF locations.
            try
            {
                string input = args.Length > 0 ? args[0] : "input.pdf";
                string output = args.Length > 1 ? args[1] : "output.pdf";

                using (var helper = new PdfAnnotationHelper())
                {
                    helper.Process(input, output, deleteAnnotations: true, flattenAnnotations: true);
                }
                Console.WriteLine("Processing completed.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
