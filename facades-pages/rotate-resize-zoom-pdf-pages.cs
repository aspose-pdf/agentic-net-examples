using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfEditorApp
{
    public static class PdfEditor
    {
        /// <summary>
        /// Applies rotation, page size, and zoom to a PDF and saves the result.
        /// </summary>
        /// <param name="inputPath">Path to the source PDF file.</param>
        /// <param name="outputPath">Path where the edited PDF will be saved.</param>
        /// <param name="rotation">Rotation angle (must be 0, 90, 180, or 270).</param>
        /// <param name="pageWidth">Desired page width (points; 1 inch = 72 points).</param>
        /// <param name="pageHeight">Desired page height (points).</param>
        /// <param name="zoom">Zoom factor (1.0 = 100%).</param>
        public static void EditPdf(string inputPath, string outputPath, int rotation, double pageWidth, double pageHeight, double zoom)
        {
            // Validate rotation value
            if (rotation != 0 && rotation != 90 && rotation != 180 && rotation != 270)
                throw new ArgumentException("Rotation must be 0, 90, 180, or 270 degrees.", nameof(rotation));

            // Ensure the input file exists
            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input PDF not found.", inputPath);

            // Use PdfPageEditor (a SaveableFacade) to modify the document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPath);

                // Set desired properties
                editor.Rotation = rotation;                     // page rotation
                editor.Zoom = (float)zoom;                      // zoom coefficient
                // PageSize constructor expects float values – cast from double
                editor.PageSize = new PageSize((float)pageWidth, (float)pageHeight);

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the edited PDF to the specified output path
                editor.Save(outputPath);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: inputPath outputPath rotation pageWidth pageHeight zoom
            if (args.Length != 6)
            {
                Console.WriteLine("Usage: PdfEditorApp <inputPath> <outputPath> <rotation> <pageWidth> <pageHeight> <zoom>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            if (!int.TryParse(args[2], out int rotation))
            {
                Console.WriteLine("Invalid rotation value.");
                return;
            }
            if (!double.TryParse(args[3], out double pageWidth) ||
                !double.TryParse(args[4], out double pageHeight) ||
                !double.TryParse(args[5], out double zoom))
            {
                Console.WriteLine("Invalid numeric parameters for page size or zoom.");
                return;
            }

            try
            {
                PdfEditor.EditPdf(inputPath, outputPath, rotation, pageWidth, pageHeight, zoom);
                Console.WriteLine("PDF edited and saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
