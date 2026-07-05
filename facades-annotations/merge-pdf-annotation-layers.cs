using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfAnnotationMergerApp
{
    public static class PdfAnnotationMerger
    {
        /// <summary>
        /// Merges annotation layers from multiple PDF files into a single PDF.
        /// The base PDF is loaded, annotations from the specified source PDFs are imported,
        /// and the result is saved to the output path.
        /// </summary>
        /// <param name="basePdfPath">Path to the PDF that will receive the annotations.</param>
        /// <param name="annotationPdfPaths">Array of PDF file paths containing annotations to import.</param>
        /// <param name="outputPdfPath">Path where the merged PDF will be saved.</param>
        public static void MergeAnnotations(string basePdfPath, string[] annotationPdfPaths, string outputPdfPath)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(basePdfPath))
                throw new ArgumentException("Base PDF path must be provided.", nameof(basePdfPath));

            if (annotationPdfPaths == null || annotationPdfPaths.Length == 0)
                throw new ArgumentException("At least one annotation PDF must be provided.", nameof(annotationPdfPaths));

            if (string.IsNullOrWhiteSpace(outputPdfPath))
                throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

            // Ensure all files exist before proceeding
            if (!File.Exists(basePdfPath))
                throw new FileNotFoundException($"Base PDF not found: {basePdfPath}");

            foreach (var path in annotationPdfPaths)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException($"Annotation PDF not found: {path}");
            }

            // Use PdfAnnotationEditor to import annotations
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            try
            {
                // Load the base document
                editor.BindPdf(basePdfPath);

                // Import annotations from the provided PDFs
                editor.ImportAnnotations(annotationPdfPaths);

                // Save the merged document
                editor.Save(outputPdfPath);
            }
            finally
            {
                // Release resources held by the editor
                editor.Close();
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point required for a console application.
        /// Accepts command‑line arguments: <basePdf> <outputPdf> <annotationPdf1> [annotationPdf2] ...
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: <basePdf> <outputPdf> <annotationPdf1> [annotationPdf2] ...");
                return;
            }

            string basePdfPath = args[0];
            string outputPdfPath = args[1];

            // Build the annotation PDF array from the remaining arguments
            string[] annotationPdfPaths = new string[args.Length - 2];
            Array.Copy(args, 2, annotationPdfPaths, 0, annotationPdfPaths.Length);

            try
            {
                PdfAnnotationMerger.MergeAnnotations(basePdfPath, annotationPdfPaths, outputPdfPath);
                Console.WriteLine("Annotations merged successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
