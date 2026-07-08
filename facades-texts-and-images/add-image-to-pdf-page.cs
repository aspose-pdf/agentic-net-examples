using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfHelper
{
    public static class PdfHelper
    {
        /// <summary>
        /// Adds an image to a specific page of a PDF document at the given rectangle coordinates.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF file.</param>
        /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
        /// <param name="imagePath">Path to the image file to be added.</param>
        /// <param name="pageNumber">1‑based page number where the image will be placed.</param>
        /// <param name="lowerLeftX">X coordinate of the lower‑left corner of the image rectangle.</param>
        /// <param name="lowerLeftY">Y coordinate of the lower‑left corner of the image rectangle.</param>
        /// <param name="upperRightX">X coordinate of the upper‑right corner of the image rectangle.</param>
        /// <param name="upperRightY">Y coordinate of the upper‑right corner of the image rectangle.</param>
        public static void AddImageToPdf(
            string inputPdfPath,
            string outputPdfPath,
            string imagePath,
            int pageNumber,
            float lowerLeftX,
            float lowerLeftY,
            float upperRightX,
            float upperRightY)
        {
            // Validate input files
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

            if (!File.Exists(imagePath))
                throw new FileNotFoundException($"Image file not found: {imagePath}");

            // Use the non‑obsolete constructor. Bind the source PDF, add the image, then save to the destination.
            using (PdfFileMend mender = new PdfFileMend())
            {
                // Bind the source PDF document.
                mender.BindPdf(inputPdfPath);

                // Add the image to the specified page and rectangle.
                // Overload: AddImage(string imageName, int pageNum, float llx, float lly, float urx, float ury)
                bool success = mender.AddImage(imagePath, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

                if (!success)
                    throw new InvalidOperationException("Failed to add image to the PDF.");

                // Save the modified PDF to the output path.
                mender.Save(outputPdfPath);
            }
        }
    }

    // Dummy entry point to satisfy the console‑application project requirement.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – the library method can be called from other code or unit tests.
        }
    }
}
