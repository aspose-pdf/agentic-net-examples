using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace PdfAnnotationUtility
{
    // Helper class that encapsulates common annotation operations.
    public class PdfAnnotationHelper : IDisposable
    {
        private readonly PdfAnnotationEditor _editor;
        private bool _isBound = false;

        // Constructor creates a new PdfAnnotationEditor instance.
        public PdfAnnotationHelper()
        {
            _editor = new PdfAnnotationEditor();
        }

        // Binds the helper to an existing PDF file.
        public void Bind(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            _editor.BindPdf(pdfPath);
            _isBound = true;
        }

        // Deletes all annotations in the bound document.
        public void DeleteAllAnnotations()
        {
            EnsureBound();
            _editor.DeleteAnnotations();
        }

        // Deletes all annotations of a specific type (e.g., "Text", "Highlight").
        public void DeleteAnnotationsByType(string annotationType)
        {
            EnsureBound();
            if (string.IsNullOrEmpty(annotationType))
                throw new ArgumentException("Annotation type must be provided.", nameof(annotationType));

            _editor.DeleteAnnotations(annotationType);
        }

        // Flattens all annotations, making them part of the page content.
        public void FlattenAllAnnotations()
        {
            EnsureBound();
            _editor.FlatteningAnnotations();
        }

        // Saves the modified PDF to the specified output path.
        public void Save(string outputPath)
        {
            EnsureBound();
            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // Ensure the directory exists.
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            _editor.Save(outputPath);
            // Close the facade to release the bound document.
            _editor.Close();
            _isBound = false;
        }

        // Helper to verify that a PDF has been bound before performing operations.
        private void EnsureBound()
        {
            if (!_isBound)
                throw new InvalidOperationException("No PDF is bound. Call Bind() first.");
        }

        // Implements IDisposable to allow using statements.
        public void Dispose()
        {
            if (_isBound)
            {
                _editor.Close();
                _isBound = false;
            }
        }
    }

    // Simple console program demonstrating the helper usage.
    class Program
    {
        public static void Main(string[] args)
        {
            // Example file paths (adjust as needed).
            const string inputPdf = "input.pdf";
            const string outputPdf = "output.pdf";

            // Ensure the input file exists for the demo.
            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input file not found: {inputPdf}");
                return;
            }

            // Use the helper within a using block to guarantee disposal.
            using (var helper = new PdfAnnotationHelper())
            {
                helper.Bind(inputPdf);

                // Delete all annotations.
                helper.DeleteAllAnnotations();

                // Alternatively, delete only text annotations:
                // helper.DeleteAnnotationsByType("Text");

                // Flatten any remaining annotations (if any).
                helper.FlattenAllAnnotations();

                // Save the result.
                helper.Save(outputPdf);
            }

            Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
        }
    }
}