using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page18.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least 18 pages (1‑based indexing)
            if (doc.Pages.Count < 18)
            {
                Console.Error.WriteLine("The document contains fewer than 18 pages.");
                return;
            }

            // Optional: ensure Times New Roman is used for missing fonts via substitution
            // This does not rely on a non‑existent DefaultFont property.
            var timesFont = FontRepository.FindFont("Times New Roman");
            if (timesFont != null)
            {
                // Substitute any missing font with Times New Roman
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Times New Roman"));
            }

            // Retrieve the 18th page
            Page page = doc.Pages[18];

            // Create a PNG device with a resolution of 300 DPI
            var pngDevice = new Aspose.Pdf.Devices.PngDevice(new Aspose.Pdf.Devices.Resolution(300));

            // Render the page to a PNG file
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
            {
                pngDevice.Process(page, outStream);
            }
        }

        Console.WriteLine($"Page 18 has been rendered to PNG at '{outputPath}'.");
    }
}
