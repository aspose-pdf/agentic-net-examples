using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfTransformationDemo
{
    public static class PdfTransformer
    {
        /// <summary>
        /// Rotates, resizes and zooms a PDF document.
        /// </summary>
        /// <param name="inputPath">Full path to the source PDF file.</param>
        /// <param name="outputPath">Full path where the transformed PDF will be saved.</param>
        /// <param name="rotationDegrees">Rotation angle (must be 0, 90, 180 or 270).</param>
        /// <param name="pageSize">Target page size (e.g., PageSize.A4, PageSize.Letter).</param>
        /// <param name="zoomFactor">Zoom coefficient (1.0 = 100%).</param>
        public static void Transform(string inputPath, string outputPath, int rotationDegrees, PageSize pageSize, float zoomFactor)
        {
            // Validate rotation value
            if (rotationDegrees != 0 && rotationDegrees != 90 && rotationDegrees != 180 && rotationDegrees != 270)
                throw new ArgumentException("Rotation must be 0, 90, 180 or 270 degrees.", nameof(rotationDegrees));

            // Use PdfPageEditor facade to edit the document.
            // The facade implements IDisposable, so wrap it in a using block.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the source PDF file.
                editor.BindPdf(inputPath);

                // Apply the requested transformations.
                editor.Rotation = rotationDegrees;   // page rotation
                editor.PageSize = pageSize;          // output page size
                editor.Zoom = zoomFactor;            // zoom factor (1.0 = 100%)

                // Save the modified document to the specified output path.
                editor.Save(outputPath);
            }
        }
    }

    // Example usage
    class Program
    {
        static void Main(string[] args)
        {
            // Example parameters – adjust as needed.
            const string inputPdf = @"C:\Docs\sample.pdf";
            const string outputPdf = @"C:\Docs\sample_transformed.pdf";
            int rotation = 90;                     // rotate 90 degrees clockwise
            PageSize targetSize = PageSize.A4;     // set page size to A4
            float zoom = 1.5f;                     // 150% zoom

            try
            {
                PdfTransformer.Transform(inputPdf, outputPdf, rotation, targetSize, zoom);
                Console.WriteLine($"PDF transformed successfully and saved to '{outputPdf}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during PDF transformation: {ex.Message}");
            }
        }
    }
}