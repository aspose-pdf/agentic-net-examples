using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Use the current working directory as the data folder (guaranteed to exist)
        string dataDir = Directory.GetCurrentDirectory();
        // Ensure the directory exists (defensive, though it always does for the current directory)
        Directory.CreateDirectory(dataDir);

        // Paths for the temporary PDF and the resulting PNG
        string pdfPath = Path.Combine(dataDir, "input.pdf");
        string pngPath = Path.Combine(dataDir, "page1.png");
        string fallbackFont = "Arial"; // Font used when the original PDF fonts are missing

        // ---------------------------------------------------------------------
        // STEP 1: Create a minimal PDF file so the example can run in a clean sandbox.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a single blank page (you could add content here if desired)
            seed.Pages.Add();
            // Save the seed PDF to the expected location
            seed.Save(pdfPath);
        }

        // ---------------------------------------------------------------------
        // STEP 2: Load the PDF and render the first page to PNG with a fallback font.
        // ---------------------------------------------------------------------
        using (Document pdfDocument = new Document(pdfPath))
        {
            if (pdfDocument.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Define the resolution for the PNG output (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            // Initialise the PNG device with the chosen resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Configure rendering options – set a default fallback font and enable font analysis
            pngDevice.RenderingOptions.DefaultFontName = fallbackFont;
            pngDevice.RenderingOptions.AnalyzeFonts = true;

            // Render the first page to a PNG file
            using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
            {
                pngDevice.Process(pdfDocument.Pages[1], pngStream);
            }
        }

        Console.WriteLine($"Page 1 rendered to PNG at: {pngPath}");
    }
}
