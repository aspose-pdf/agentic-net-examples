using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    public static class PdfImageRemover
    {
        /// <summary>
        /// Removes all images from the specified PDF file and saves the result to a new file.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        /// <param name="outputPdfPath">Path where the image‑free PDF will be saved.</param>
        public static void RemoveAllImages(string inputPdfPath, string outputPdfPath)
        {
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException($"Input file not found: {inputPdfPath}");

            // PdfContentEditor implements IDisposable, so we wrap it in a using block.
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the PDF document.
                editor.BindPdf(inputPdfPath);

                // Delete every image from the document.
                editor.DeleteImage();

                // Save the modified PDF to the output path.
                editor.Save(outputPdfPath);
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point required for a console application.
        /// Expects two arguments: input PDF path and output PDF path.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: PdfImageRemover <inputPdfPath> <outputPdfPath>");
                return;
            }

            try
            {
                PdfImageRemover.RemoveAllImages(args[0], args[1]);
                Console.WriteLine("All images removed successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}