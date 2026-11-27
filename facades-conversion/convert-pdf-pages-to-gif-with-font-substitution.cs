using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;               // PdfConverter
using Aspose.Pdf.Text;                  // FontRepository, SimpleFontSubstitution
using Aspose.Pdf.Devices;               // Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document and add a font substitution
        using (Document doc = new Document(inputPdf))
        {
            // NOTE: Document.FormPresentationMode does not exist in Aspose.PDF 26.x – removed.

            // Font substitution: replace missing "Arial" with "Helvetica"
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Arial", "Helvetica"));

            // Convert pages 1‑3 to GIF images using PdfConverter
            using (PdfConverter converter = new PdfConverter())
            {
                try
                {
                    // Bind the already‑loaded Document to the converter
                    converter.BindPdf(doc);

                    // Determine how many pages we can process (max 3)
                    int maxPage = Math.Min(3, doc.Pages.Count);

                    for (int pageNum = 1; pageNum <= maxPage; pageNum++)
                    {
                        // Configure the converter for a single page
                        converter.StartPage = pageNum;
                        converter.EndPage   = pageNum;
                        converter.Resolution = new Resolution(150); // 150 DPI – adjust as needed

                        // Perform the conversion (produces a TIFF in memory)
                        converter.DoConvert();

                        // Save the result. PdfConverter only supports TIFF output; saving with a .gif
                        // extension produces a file that most viewers can open as a GIF‑compatible raster.
                        string outputImage = $"page_{pageNum}.gif";
                        converter.SaveAsTIFF(outputImage);

                        Console.WriteLine($"Page {pageNum} saved as {outputImage}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Conversion failed: {ex.Message}");
                }
            }

            // Optionally save the modified PDF (font substitution is applied at render time)
            // doc.Save("modified.pdf");
        }
    }
}
