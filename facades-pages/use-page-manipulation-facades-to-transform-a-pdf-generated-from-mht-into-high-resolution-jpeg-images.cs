using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Added for Resolution struct

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string mhtInputPath = "input.mht";
        const string outputFolder = "OutputImages";

        // Verify input file exists
        if (!File.Exists(mhtInputPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtInputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // -----------------------------------------------------------------
        // Step 1: Convert the MHT file to a PDF document.
        // HtmlLoadOptions can be used because MHT is a web‑archive format.
        // The Document is wrapped in a using block for deterministic disposal.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(mhtInputPath, new HtmlLoadOptions()))
        {
            // Save the intermediate PDF to a temporary file.
            // PdfConverter works with a file path, so we need a physical PDF.
            string tempPdfPath = Path.Combine(outputFolder, "temp.pdf");
            pdfDoc.Save(tempPdfPath);

            // -----------------------------------------------------------------
            // Step 2: Use PdfConverter (a facade) to render each PDF page
            //         to a high‑resolution JPEG image.
            // -----------------------------------------------------------------
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the temporary PDF file.
                converter.BindPdf(tempPdfPath);

                // Set a high resolution (e.g., 300 DPI) for better image quality.
                converter.Resolution = new Resolution(300);

                // Prepare the converter.
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate over all pages and save each as a JPEG image.
                while (converter.HasNextImage())
                {
                    string jpegPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpeg");
                    // GetNextImage(string) uses JPEG as the default format.
                    converter.GetNextImage(jpegPath);
                    pageNumber++;
                }
            }

            // Optional: delete the temporary PDF file.
            try { File.Delete(Path.Combine(outputFolder, "temp.pdf")); } catch { }
        }

        Console.WriteLine("Conversion completed. JPEG images are saved in: " + outputFolder);
    }
}
