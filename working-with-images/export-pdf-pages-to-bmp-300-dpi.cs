using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class ExportPdfToBmp
{
    static void Main()
    {
        // Resolve a concrete folder where the PDF resides and BMPs will be written.
        // Here we use a "Data" sub‑folder of the current working directory.
        string dataDir = Path.Combine(Environment.CurrentDirectory, "Data");
        Directory.CreateDirectory(dataDir); // ensure the folder exists

        // Name of the source PDF – replace "sample.pdf" with your actual file name.
        string pdfFileName = "sample.pdf";
        string pdfPath = Path.Combine(dataDir, pdfFileName);

        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            Console.WriteLine("Place the PDF in the above folder or change the file name/path in the code.");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Desired resolution – 300 DPI for high‑quality rasterisation
            Resolution resolution = new Resolution(300);

            // Create a BMP device with the specified resolution.
            // The BmpDevice constructor that accepts a Resolution uses that DPI for rendering.
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Process each page (Aspose.Pdf uses 1‑based page indexing)
            for (int pageNum = 1; pageNum <= pdfDocument.Pages.Count; pageNum++)
            {
                string outBmpPath = Path.Combine(dataDir, $"image{pageNum}_out.bmp");

                // Write the BMP image to a file stream
                using (FileStream bmpStream = new FileStream(outBmpPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDocument.Pages[pageNum], bmpStream);
                }

                Console.WriteLine($"Page {pageNum} saved as BMP: {outBmpPath}");
            }
        }
    }
}
