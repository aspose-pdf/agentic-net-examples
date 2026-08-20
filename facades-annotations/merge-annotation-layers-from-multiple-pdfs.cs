using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    public static class PdfAnnotationMerger
    {
        /// <summary>
        /// Merges annotation layers from multiple source PDFs into a target PDF.
        /// The resulting PDF contains all annotations from the source documents
        /// combined with the original content of the target PDF.
        /// </summary>
        /// <param name="targetPdfPath">Path to the PDF that will receive the annotations.</param>
        /// <param name="sourcePdfPaths">Array of PDF file paths whose annotations will be imported.</param>
        /// <param name="outputPdfPath">Path where the merged PDF will be saved.</param>
        public static void MergeAnnotationLayers(string targetPdfPath, string[] sourcePdfPaths, string outputPdfPath)
        {
            // Validate input files
            if (!File.Exists(targetPdfPath))
                throw new FileNotFoundException($"Target PDF not found: {targetPdfPath}");

            foreach (string src in sourcePdfPaths)
            {
                if (!File.Exists(src))
                    throw new FileNotFoundException($"Source PDF not found: {src}");
            }

            // Use PdfAnnotationEditor to bind the target PDF and import annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the target PDF document
                editor.BindPdf(targetPdfPath);

                // Import all annotations from the source PDFs (all annotation types)
                editor.ImportAnnotations(sourcePdfPaths);

                // Save the combined document to the specified output path
                editor.Save(outputPdfPath);
            }
        }
    }

    // Minimal entry point required for a console‑application build.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Example usage (commented out – replace with real file paths to run).
            // string target = "target.pdf";
            // string[] sources = { "source1.pdf", "source2.pdf" };
            // string output = "merged.pdf";
            // PdfAnnotationMerger.MergeAnnotationLayers(target, sources, output);
        }
    }
}