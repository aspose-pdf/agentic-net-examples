using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class AnnotationMerger
    {
        /// <summary>
        /// Merges annotation layers from multiple source PDFs into a target PDF.
        /// The resulting PDF contains the original content of <paramref name="targetPdfPath"/>
        /// plus all annotations from the files listed in <paramref name="sourcePdfPaths"/>.
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
            // This overload imports every annotation type from each source file
            editor.ImportAnnotations(sourcePdfPaths);

            // Save the combined document to the specified output path
            editor.Save(outputPdfPath);
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point required for a console application.
        /// Demonstrates how to call <see cref="AnnotationMerger.MergeAnnotationLayers"/>.
        /// </summary>
        static void Main(string[] args)
        {
            // Expected arguments: targetPdfPath outputPdfPath sourcePdfPath1 [sourcePdfPath2 ...]
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: <targetPdfPath> <outputPdfPath> <sourcePdfPath1> [sourcePdfPath2] ...");
                return;
            }

            string targetPdfPath = args[0];
            string outputPdfPath = args[1];
            // C# 8 range operator to get the remaining arguments as source PDFs
            string[] sourcePdfPaths = args[2..];

            try
            {
                AnnotationMerger.MergeAnnotationLayers(targetPdfPath, sourcePdfPaths, outputPdfPath);
                Console.WriteLine("Annotation layers merged successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error merging annotations: {ex.Message}");
            }
        }
    }
}
