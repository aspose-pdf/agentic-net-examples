using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    /// <summary>
    /// Helper class that encapsulates binding a PDF, deleting all annotations,
    /// flattening remaining annotations, and saving the result.
    /// </summary>
    public static class AnnotationHelper
    {
        /// <summary>
        /// Processes annotations in the specified PDF file.
        /// </summary>
        /// <param name="inputPath">Full path to the source PDF.</param>
        /// <param name="outputPath">Full path where the processed PDF will be saved.</param>
        public static void ProcessAnnotations(string inputPath, string outputPath)
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"Input file not found: {inputPath}");

            // PdfAnnotationEditor implements IDisposable, so use a using block.
            using (Aspose.Pdf.Facades.PdfAnnotationEditor editor = new Aspose.Pdf.Facades.PdfAnnotationEditor())
            {
                // Bind the PDF document to the editor.
                editor.BindPdf(inputPath);

                // Remove all existing annotations.
                editor.DeleteAnnotations();

                // Flatten any remaining annotations into the page content.
                editor.FlatteningAnnotations();

                // Save the modified PDF.
                editor.Save(outputPath);
            }
        }
    }

    /// <summary>
    /// Simple console entry point to demonstrate the helper usage.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input PDF path and output PDF path.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: AsposePdfApi <input.pdf> <output.pdf>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                AnnotationHelper.ProcessAnnotations(inputPath, outputPath);
                Console.WriteLine($"Annotations processed and saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}