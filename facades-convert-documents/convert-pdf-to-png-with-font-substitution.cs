using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PngPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Font substitution: replace Helvetica with Times New Roman for missing fonts
        FontRepository.Substitutions.Clear();
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Times New Roman"));

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Set the desired resolution (DPI) for the PNG images
                var resolution = new Resolution(150);
                var pngDevice = new PngDevice(resolution);

                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                    using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
                    {
                        // Render the current page to PNG and write to the file stream
                        pngDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                    }
                    Console.WriteLine($"Saved page {pageNumber} as PNG: {outputPath}");
                }
            }

            Console.WriteLine("PDF to PNG conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
