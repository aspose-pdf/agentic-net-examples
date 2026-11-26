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
        const string outputPngPath = "page29.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Register Lucida Console font as a substitution for any missing font.
        string lucidaFontPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
            "lucon.ttf");
        if (File.Exists(lucidaFontPath))
        {
            // Substitute any missing font with Lucida Console.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", lucidaFontPath));
        }
        else
        {
            Console.Error.WriteLine("Lucida Console font not found; default system fonts will be used.");
        }

        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                if (pdfDoc.Pages.Count < 29)
                {
                    Console.Error.WriteLine("The PDF does not contain 29 pages.");
                    return;
                }

                Page page = pdfDoc.Pages[29];

                // Create a PNG device with 250 DPI resolution.
                PngDevice pngDevice = new PngDevice(new Resolution(250))
                {
                    TransparentBackground = true // optional, makes background transparent
                };

                using (FileStream outStream = new FileStream(outputPngPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(page, outStream);
                }
            }

            Console.WriteLine($"Page 29 has been saved as PNG to '{outputPngPath}'.");
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("Image conversion requires GDI+ and is only supported on Windows.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
