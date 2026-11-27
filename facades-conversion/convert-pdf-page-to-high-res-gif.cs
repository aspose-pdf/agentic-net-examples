using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputGif = "page7.gif";
        const string fontsFolder = "fonts"; // folder containing custom fonts

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Register all fonts from the specified folder (including subfolders)
        if (Directory.Exists(fontsFolder))
        {
            // In recent Aspose.Pdf versions FontRepository.AddFontFolder was removed.
            // Use FontRepository.Substitutions with SimpleFontSubstitution for each font file.
            var fontFiles = Directory.GetFiles(fontsFolder, "*.*", SearchOption.AllDirectories)
                .Where(f => f.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".otf", StringComparison.OrdinalIgnoreCase));

            foreach (var fontFile in fontFiles)
            {
                // Use the file name (without extension) as the font name for substitution.
                var fontName = Path.GetFileNameWithoutExtension(fontFile);
                FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontName, fontFile));
            }
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Convert only page 7 to an image at 300 DPI.
            // PdfConverter does not expose ConvertPageToImage; instead use a device class.
            // PngDevice is used as an intermediate format because System.Drawing can
            // easily re‑encode PNG to GIF while preserving the resolution.
            using (MemoryStream pngStream = new MemoryStream())
            {
                // Create a PNG device with the required resolution.
                PngDevice pngDevice = new PngDevice(new Resolution(300));
                // Process the specific page (pages are 1‑based).
                pngDevice.Process(doc.Pages[7], pngStream);
                pngStream.Position = 0; // rewind for reading

                // Load the PNG into System.Drawing.Image and save as GIF.
                using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(pngStream))
                {
                    sysImg.Save(outputGif, System.Drawing.Imaging.ImageFormat.Gif);
                }
            }
        }

        Console.WriteLine($"Page 7 has been saved as GIF: {outputGif}");
    }
}
