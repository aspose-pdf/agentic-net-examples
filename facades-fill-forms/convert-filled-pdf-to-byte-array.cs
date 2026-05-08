using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfUtility
    {
        /// <summary>
        /// Loads a filled PDF from the specified file path and returns its content as a byte array.
        /// The method uses Aspose.Pdf.Facades.PdfViewer to avoid writing any intermediate files.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF file.</param>
        /// <returns>Byte array containing the PDF data.</returns>
        public static byte[] GetPdfBytes(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            // MemoryStream will hold the PDF data in memory.
            using (MemoryStream outputStream = new MemoryStream())
            {
                // PdfViewer is a facade that can bind a PDF and save it to a stream.
                using (PdfViewer viewer = new PdfViewer())
                {
                    // Load the PDF from file.
                    viewer.BindPdf(pdfPath);

                    // Save the PDF directly into the MemoryStream.
                    viewer.Save(outputStream);
                }

                // Ensure the stream position is reset before reading.
                outputStream.Position = 0;

                // Return the underlying byte array.
                return outputStream.ToArray();
            }
        }
    }

    // Dummy entry point to satisfy the console‑application requirement.
    // In a real library project this class would be omitted or the project type changed to Class Library.
    public class Program
    {
        public static void Main(string[] args)
        {
            // Example usage (can be removed in production).
            // string path = "filled.pdf";
            // byte[] pdfBytes = PdfUtility.GetPdfBytes(path);
            // Console.WriteLine($"PDF size: {pdfBytes.Length} bytes");
        }
    }
}
