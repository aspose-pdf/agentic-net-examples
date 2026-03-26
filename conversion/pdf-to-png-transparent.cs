using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Resolve the input and output directories.
        // ---------------------------------------------------------------------
        // Use the folder that contains the executable as the base directory.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        // Input PDF file – replace "sample.pdf" with the actual file name you
        // want to convert. The file must exist in the base directory (or a sub‑
        // folder you specify).
        string pdfFileName = "sample.pdf";
        // Full path to the PDF document.
        string pdfPath = Path.Combine(baseDir, pdfFileName);

        // Verify that the PDF file exists; if not, give a clear message.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Output folder – we create a sub‑folder called "output" under the base
        // directory. If it does not exist we create it so the FileStream later
        // does not throw a DirectoryNotFoundException.
        string outputDir = Path.Combine(baseDir, "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // ---------------------------------------------------------------------
        // Load the PDF document (lifecycle rule: use "using" to ensure disposal).
        // ---------------------------------------------------------------------
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Define image resolution (e.g., 300 DPI).
            Resolution resolution = new Resolution(300);

            // Create a PngDevice with the desired resolution and enable a
            // transparent background.
            PngDevice pngDevice = new PngDevice(resolution)
            {
                TransparentBackground = true
            };

            // Convert each page to a PNG image.
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"image{pageNumber}_out.png");
                using (FileStream pngStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
                Console.WriteLine($"Page {pageNumber} saved to {outPath}");
            }
        }
    }
}
