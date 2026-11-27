using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Add a custom font substitution rule before binding the PDF.
        // Use SimpleFontSubstitution as the collection expects this type.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("MyCustomFont", "Arial"));

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the PdfConverter (facade) and bind the document.
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(pdfDoc);

                // Convert pages 4 and 5 to separate TIFF files.
                for (int pageNumber = 4; pageNumber <= 5; pageNumber++)
                {
                    string outputTiffPath = $"page_{pageNumber}.tiff";

                    // Configure the converter for a single page.
                    converter.StartPage = pageNumber;
                    converter.EndPage   = pageNumber;
                    // Optional: set resolution (e.g., 150 DPI).
                    converter.Resolution = new Resolution(150);

                    // Perform the conversion and save the result as TIFF.
                    converter.DoConvert();
                    converter.SaveAsTIFF(outputTiffPath);

                    Console.WriteLine($"Page {pageNumber} saved as {outputTiffPath}");
                }
            }
        }
    }
}
