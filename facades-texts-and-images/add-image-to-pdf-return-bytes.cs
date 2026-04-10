using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileMend (facade for adding images)

namespace PdfProcessing
{
    /// <summary>
    /// Provides PDF manipulation utilities.
    /// </summary>
    public static class PdfImageProcessor
    {
        /// <summary>
        /// Adds an image to the first page of the specified PDF and returns the modified PDF as a byte array.
        /// The caller can write the returned bytes to any HTTP response (ASP.NET, ASP.NET Core, etc.).
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF file.</param>
        /// <param name="imagePath">Path to the image file to be added.</param>
        /// <returns>Byte array containing the modified PDF.</returns>
        public static byte[] AddImageAndGetBytes(string pdfPath, string imagePath)
        {
            // Validate input files
            if (!File.Exists(pdfPath) || !File.Exists(imagePath))
                throw new FileNotFoundException("Source PDF or image not found.");

            // PdfFileMend is a saveable facade that works with existing PDFs.
            // It implements IDisposable, so wrap it in a using block.
            using (PdfFileMend pdfMend = new PdfFileMend())
            {
                // Load the source PDF into the facade.
                pdfMend.BindPdf(pdfPath);

                // Add the image to page 1.
                // Parameters: image file path, page number (1‑based), lower‑left X, lower‑left Y,
                // upper‑right X, upper‑right Y (all in points, 1/72 inch).
                pdfMend.AddImage(imagePath, 1, 100f, 500f, 300f, 700f);

                // Save the modified PDF into a memory stream.
                using (MemoryStream pdfStream = new MemoryStream())
                {
                    pdfMend.Save(pdfStream); // SaveableFacade.Save(Stream)
                    return pdfStream.ToArray(); // Return the PDF bytes
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // In a real library you would change the project type to a class‑library, but adding a
    // minimal Main method is the quickest way to resolve CS5001 without altering the project file.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – the library is intended to be used from ASP.NET controllers or other callers.
            // Example (commented out) of how to invoke the processor:
            // byte[] pdfBytes = PdfImageProcessor.AddImageAndGetBytes("sample.pdf", "logo.png");
        }
    }
}
