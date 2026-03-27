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

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPath))
        {
            // Enable font substitution for any missing fonts before conversion
            // Map missing fonts to a known substitute (e.g., Arial). Add more mappings as required.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont", "Arial"));
            // Example of adding another substitution:
            // FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Arial"));

            // Convert each page to a PNG image
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputImage = $"page_{pageNumber}.png";
                using (FileStream imageStream = new FileStream(outputImage, FileMode.Create))
                {
                    PngDevice pngDevice = new PngDevice();
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
                Console.WriteLine($"Saved {outputImage}");
            }
        }
    }
}
