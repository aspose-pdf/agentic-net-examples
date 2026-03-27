using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

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

        // Register a font substitution: replace missing Helvetica with Times New Roman.
        // FontRepository.Substitutions is a static collection, so it must be configured before the document is loaded.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Times New Roman"));

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPath))
        {
            int pageCount = pdfDocument.Pages.Count;
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                string outputImage = $"page_{pageNumber}.png";
                using (FileStream imageStream = new FileStream(outputImage, FileMode.Create))
                {
                    PngDevice pngDevice = new PngDevice();
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }
                Console.WriteLine($"Saved page {pageNumber} as {outputImage}");
            }
        }
    }
}
