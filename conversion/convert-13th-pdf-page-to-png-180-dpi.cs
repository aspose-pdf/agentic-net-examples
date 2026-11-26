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
        const string outputPath = "page13.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Register Lucida Console for missing‑font substitution if it exists on the system.
        // Use FontRepository.Substitutions with SimpleFontSubstitution (AddFont was removed in newer versions).
        var lucidaFont = FontRepository.FindFont("Lucida Console");
        if (lucidaFont != null)
        {
            // Substitute any missing font with Lucida Console. The second argument is the path to the font file.
            // If the exact path is unknown, providing the font name works for compilation; runtime will resolve if possible.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Lucida Console"));
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document actually has a 13th page.
            if (doc.Pages.Count < 13)
            {
                Console.Error.WriteLine("The document contains fewer than 13 pages.");
                return;
            }

            Page page = doc.Pages[13];

            // Create a PNG device with the required resolution (180 DPI).
            var resolution = new Resolution(180);
            var pngDevice = new PngDevice(resolution);

            // Render the selected page to a PNG file.
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                pngDevice.Process(page, imageStream);
            }
        }

        Console.WriteLine($"Page 13 saved as PNG to '{outputPath}'.");
    }
}
