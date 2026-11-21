using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // FontRepository, SimpleFontSubstitution
using Aspose.Pdf.Devices; // Resolution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "TiffPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Substitute Courier with Liberation Mono for better rendering.
        // Must be set before loading the document.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Courier", "LiberationMono"));

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialise the PDF converter (facade)
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(pdfDoc);
                converter.StartPage = 1;
                converter.EndPage = pdfDoc.Pages.Count;
                converter.Resolution = new Resolution(150); // 150 DPI – adjust as needed

                // Perform the conversion
                converter.DoConvert();

                // Save all pages as a multi‑page TIFF file
                string tiffPath = Path.Combine(outputFolder, "output.tiff");
                converter.SaveAsTIFF(tiffPath);
                Console.WriteLine($"Saved TIFF: {tiffPath}");
            }
        }

        Console.WriteLine("PDF to TIFF conversion completed.");
    }
}
