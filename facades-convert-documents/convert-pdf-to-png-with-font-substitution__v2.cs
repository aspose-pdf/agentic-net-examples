using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "PngPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load PDF
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Enable font substitution if the property exists (some older versions lack it)
                var fontSubProp = pdfDoc.GetType().GetProperty("FontSubstitutionEnabled");
                if (fontSubProp != null && fontSubProp.CanWrite)
                {
                    fontSubProp.SetValue(pdfDoc, true);
                }

                // Set the desired resolution (DPI) for the PNG images
                var resolution = new Resolution(300);
                var pngDevice = new PngDevice(resolution);

                // Convert each page to a separate PNG file (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.png");
                    using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                    {
                        pngDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                    }
                    Console.WriteLine($"Saved page {pageNumber} as PNG → {outPath}");
                }
            }

            Console.WriteLine("PDF to PNG conversion completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
