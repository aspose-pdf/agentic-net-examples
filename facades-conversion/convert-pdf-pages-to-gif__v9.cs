using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "GifPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Register custom fonts via FontRepository.Substitutions before conversion
            string customFontPath = "MyFont.ttf";
            if (File.Exists(customFontPath))
            {
                // Use the file name (without extension) as the font family name or a wildcard "*" for any missing font
                string fontFamily = Path.GetFileNameWithoutExtension(customFontPath);
                FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontFamily, customFontPath));
            }

            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded document to the converter
                converter.BindPdf(doc);

                // Configure the page range BEFORE any conversion work
                converter.StartPage = 1;
                // Example: convert only the first 5 pages (or fewer if the document has less)
                converter.EndPage = Math.Min(5, doc.Pages.Count);

                // Optional: set the resolution for image conversion (150 DPI is a good default)
                converter.Resolution = new Resolution(150);

                // Convert each page in the selected range to a GIF image using GifDevice
                for (int pageNum = converter.StartPage; pageNum <= converter.EndPage; pageNum++)
                {
                    // GifDevice works with a Stream; it does not rely on System.Drawing
                    var gifDevice = new GifDevice(converter.Resolution);
                    string outPath = Path.Combine(outputDir, $"Page_{pageNum}.gif");

                    using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        gifDevice.Process(doc.Pages[pageNum], outStream);
                    }

                    Console.WriteLine($"Saved GIF: {outPath}");
                }
            }
        }
    }
}
