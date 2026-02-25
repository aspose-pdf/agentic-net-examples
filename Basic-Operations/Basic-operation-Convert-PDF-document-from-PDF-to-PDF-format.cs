using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define input and output PDF file paths.
            // Adjust these paths as needed for your environment.
            string inputPdfPath = "input.pdf";
            string outputPdfPath = "output.pdf";

            // Load the source PDF document.
            // Using Aspose.Pdf.Document for loading.
            Document pdfDocument = new Document(inputPdfPath);

            // Save the document back to PDF format.
            // This effectively copies the PDF, demonstrating a PDF‑to‑PDF conversion.
            pdfDocument.Save(outputPdfPath);
        }
    }
}