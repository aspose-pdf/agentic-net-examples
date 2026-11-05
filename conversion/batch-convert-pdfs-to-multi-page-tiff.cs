using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputFolder = @"C:\PdfInput";
        const string outputTiff = @"C:\PdfOutput\combined.tiff";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found to process.");
            return;
        }

        try
        {
            // Load the first PDF as the base document
            using (Document target = new Document(pdfFiles[0]))
            {
                // Append pages from the remaining PDFs
                for (int i = 1; i < pdfFiles.Length; i++)
                {
                    using (Document source = new Document(pdfFiles[i]))
                    {
                        target.Pages.Add(source.Pages);
                    }
                }

                // Convert the combined PDF to a multi‑page TIFF using TiffDevice.
                // Default DPI (300) and default compression are used.
                Resolution resolution = new Resolution(300);
                TiffSettings tiffSettings = new TiffSettings(); // default compression
                TiffDevice tiffDevice = new TiffDevice(resolution, tiffSettings);

                // TiffDevice does not implement IDisposable, so we only dispose the stream.
                using (FileStream tiffStream = new FileStream(outputTiff, FileMode.Create, FileAccess.Write))
                {
                    tiffDevice.Process(target, 1, target.Pages.Count, tiffStream);
                }
            }

            Console.WriteLine($"Successfully created TIFF archive: {outputTiff}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
