using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfEditDemo
{
    /// <summary>
    /// Demonstrates how to edit a PDF document using Aspose.Pdf.Facades.
    /// The example loads an existing PDF, adds an image to the first page,
    /// and saves the modified document.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        public static void Main(string[] args)
        {
            const string inputPdfPath = "input.pdf";
            const string imagePath    = "logo.png";
            const string outputPdfPath = "output.pdf";

            // Verify that the source PDF and image files exist.
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
                return;
            }

            if (!File.Exists(imagePath))
            {
                Console.Error.WriteLine($"Error: Image file not found – {imagePath}");
                return;
            }

            // -----------------------------------------------------------------
            // Load the PDF document using the PdfFileMend facade.
            // -----------------------------------------------------------------
            PdfFileMend pdfMend = new PdfFileMend();
            pdfMend.BindPdf(inputPdfPath); // Binds the source PDF for editing.

            // -----------------------------------------------------------------
            // Add an image to page 1.
            // Coordinates are specified as left, bottom, right, top (points).
            // -----------------------------------------------------------------
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // AddImage(Stream image, int pageNumber, float llx, float lly, float urx, float ury)
                pdfMend.AddImage(imgStream, 1, 100f, 500f, 300f, 700f);
            }

            // -----------------------------------------------------------------
            // Save the edited PDF to a new file.
            // -----------------------------------------------------------------
            pdfMend.Save(outputPdfPath);
            pdfMend.Close(); // Release resources held by the facade.

            Console.WriteLine($"Successfully saved edited PDF to '{outputPdfPath}'.");
        }
    }
}