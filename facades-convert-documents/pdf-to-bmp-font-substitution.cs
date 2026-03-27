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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Register a font substitution: replace Times New Roman with Calibri
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Calibri"));

        using (Document doc = new Document(inputPath))
        {
            // Define image resolution (300 DPI)
            Resolution resolution = new Resolution(300);
            BmpDevice bmpDevice = new BmpDevice(resolution);

            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];
                string outputFile = $"page_{pageNumber}.bmp";
                bmpDevice.Process(page, outputFile);
                Console.WriteLine($"Saved {outputFile}");
            }
        }
    }
}