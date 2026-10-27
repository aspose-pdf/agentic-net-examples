using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "OutputImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // 200 DPI resolution – the Resolution struct lives in Aspose.Pdf.Devices
                var resolution = new Resolution(200);
                // BmpDevice does NOT implement IDisposable, so instantiate it without a using block
                var bmpDevice = new BmpDevice(resolution);

                for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    string outPath = Path.Combine(outputFolder, $"Page_{pageNumber}.bmp");
                    // Save the page directly to a BMP file
                    using (FileStream imageStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        bmpDevice.Process(doc.Pages[pageNumber], imageStream);
                    }
                }
            }

            Console.WriteLine("PDF successfully converted to BMP images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
