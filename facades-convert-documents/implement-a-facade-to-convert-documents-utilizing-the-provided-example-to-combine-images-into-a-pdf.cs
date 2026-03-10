using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace DocumentConversionFacade
{
    /// <summary>
    /// Provides functionality to combine multiple image files into a single PDF document
    /// using Aspose.Pdf.Facades (PdfFileMend facade).
    /// </summary>
    public static class ImageToPdfConverter
    {
        /// <summary>
        /// Combines the specified image files into a PDF.
        /// Each image is placed on its own page.
        /// </summary>
        /// <param name="imagePaths">Array of full paths to image files (BMP, JPEG, PNG, TIFF).</param>
        /// <param name="outputPdfPath">Full path where the resulting PDF will be saved.</param>
        public static void ConvertImagesToPdf(string[] imagePaths, string outputPdfPath)
        {
            if (imagePaths == null) throw new ArgumentNullException(nameof(imagePaths));
            if (string.IsNullOrWhiteSpace(outputPdfPath)) throw new ArgumentException("Output path must be provided.", nameof(outputPdfPath));

            // Ensure the output directory exists
            string outputDir = Path.GetDirectoryName(outputPdfPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Create a new empty PDF document
            using (Document pdfDoc = new Document())
            {
                // Bind the document to the PdfFileMend facade for image insertion
                using (PdfFileMend mend = new PdfFileMend(pdfDoc))
                {
                    foreach (string imgPath in imagePaths)
                    {
                        if (!File.Exists(imgPath))
                        {
                            Console.Error.WriteLine($"Image not found and will be skipped: {imgPath}");
                            continue;
                        }

                        // Add a new blank page for the current image
                        pdfDoc.Pages.Add();

                        // The newly added page is the last one in the collection (1‑based indexing)
                        int pageNumber = pdfDoc.Pages.Count;

                        // Add the image to the page.
                        // Parameters: image file path, page number, left, bottom, width, height.
                        // Setting width and height to 0 preserves the original image dimensions.
                        mend.AddImage(imgPath, pageNumber, 0, 0, 0, 0);
                    }

                    // Save the assembled PDF document
                    pdfDoc.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"Images successfully combined into PDF: {outputPdfPath}");
        }
    }

    // Example usage
    class Program
    {
        static void Main()
        {
            // Define input images and output PDF
            string[] images = new[]
            {
                "C:\\Images\\photo1.jpg",
                "C:\\Images\\photo2.png",
                "C:\\Images\\diagram.tif"
            };
            string outputPdf = "C:\\Output\\combined.pdf";

            // Perform conversion
            ImageToPdfConverter.ConvertImagesToPdf(images, outputPdf);
        }
    }
}