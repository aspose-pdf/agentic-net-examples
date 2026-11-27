using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices; // Added for Resolution

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Pages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Loop over the required pages (3‑6)
        for (int pageNumber = 3; pageNumber <= 6; pageNumber++)
        {
            // Each iteration uses a fresh PdfConverter instance – this keeps the workflow simple
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF document
                converter.BindPdf(inputPdfPath);

                // ---- Font substitution (custom font mapping) ----
                // Map a missing font to an available one on the machine.
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("MyCustomFont", "Arial"));

                // Restrict conversion to a single page
                converter.StartPage = pageNumber;
                converter.EndPage   = pageNumber;

                // Set the rendering resolution (dots per inch)
                converter.Resolution = new Resolution(150);

                // Perform the conversion preparation step
                converter.DoConvert();

                // Save the rendered page as a temporary TIFF file (PdfConverter only supports TIFF)
                string tempTiffPath = Path.Combine(outputFolder, $"Page_{pageNumber}.tiff");
                converter.SaveAsTIFF(tempTiffPath);

                // Convert the TIFF to PNG using System.Drawing. The temporary TIFF is then deleted.
                using (Image img = Image.FromFile(tempTiffPath))
                {
                    string pngPath = Path.Combine(outputFolder, $"Page_{pageNumber}.png");
                    img.Save(pngPath, ImageFormat.Png);
                    Console.WriteLine($"Saved page {pageNumber} as {pngPath}");
                }

                // Clean up the intermediate TIFF file
                File.Delete(tempTiffPath);
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
