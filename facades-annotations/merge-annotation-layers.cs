using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfAnnotationMergerApp
{
    /// <summary>
    /// Provides functionality to merge annotation layers from multiple PDFs into a single PDF.
    /// </summary>
    public static class AnnotationMerger
    {
        /// <summary>
        /// Merges annotation layers from multiple source PDFs into a target PDF.
        /// The resulting PDF contains all annotations from the source documents.
        /// </summary>
        /// <param name="targetPdfPath">Path to the PDF that will receive the annotations.</param>
        /// <param name="sourcePdfPaths">Array of PDF file paths whose annotations will be merged.</param>
        /// <param name="outputPdfPath">Path where the merged PDF will be saved.</param>
        public static void MergeAnnotationLayers(string targetPdfPath, string[] sourcePdfPaths, string outputPdfPath)
        {
            // Validate input files
            if (!File.Exists(targetPdfPath))
                throw new FileNotFoundException($"Target PDF not found: {targetPdfPath}");

            foreach (var src in sourcePdfPaths)
            {
                if (!File.Exists(src))
                    throw new FileNotFoundException($"Source PDF not found: {src}");
            }

            // Initialize the PdfAnnotationEditor facade
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Bind the target PDF to the editor
            editor.BindPdf(targetPdfPath);

            // Import all annotations from the source PDFs
            // This imports every annotation type; you can filter by AnnotationType[] if needed.
            editor.ImportAnnotations(sourcePdfPaths);

            // Save the merged document to the specified output path
            editor.Save(outputPdfPath);

            // Close the editor to release resources
            editor.Close();
        }
    }

    /// <summary>
    /// Entry point required for a console‑application build.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Simple demonstration of how the merger could be invoked.
            // Expected arguments:
            //   0 – target PDF path (the PDF that will receive annotations)
            //   1 – comma‑ or semicolon‑separated list of source PDF paths
            //   2 – output PDF path
            if (args.Length >= 3)
            {
                string targetPdf = args[0];
                string[] sourcePdfs = args[1]
                    .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                string outputPdf = args[2];

                try
                {
                    AnnotationMerger.MergeAnnotationLayers(targetPdf, sourcePdfs, outputPdf);
                    Console.WriteLine("Annotation layers merged successfully.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: <targetPdf> <sourcePdf1,sourcePdf2,...> <outputPdf>");
            }
        }
    }
}
