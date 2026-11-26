using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page9.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Register Tahoma font if it exists on the system
            string tahomaPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
                "tahoma.ttf");

            if (File.Exists(tahomaPath))
            {
                // Substitute common missing fonts with Tahoma using the FontRepository API
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("Arial", tahomaPath));
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", tahomaPath));
                // Add more substitutions here if required
            }

            // Create a PNG device with a resolution of 250 DPI
            Resolution resolution = new Resolution(250);
            PngDevice pngDevice = new PngDevice(resolution)
            {
                // Use the CropBox coordinate type to match the visible page area
                CoordinateType = PageCoordinateType.CropBox
            };

            // Ensure the document has at least 9 pages
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document does not contain a 9th page.");
                return;
            }

            // Convert the 9th page (1‑based index) to PNG using the two‑argument overload.
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                Page page9 = doc.Pages[9];
                pngDevice.Process(page9, imageStream);
            }
        }

        Console.WriteLine($"Page 9 saved as PNG to '{outputPath}'.");
    }
}
