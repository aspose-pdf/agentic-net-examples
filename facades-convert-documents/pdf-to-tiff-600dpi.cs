using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(doc);
                converter.StartPage = 1;
                converter.EndPage = doc.Pages.Count;
                // Set DPI using a Resolution object (600 DPI for detailed graphics)
                converter.Resolution = new Resolution(600);
                // Perform the conversion before saving
                converter.DoConvert();
                converter.SaveAsTIFF(outputPath);
                converter.Close();
            }
        }

        Console.WriteLine($"PDF converted to TIFF at '{outputPath}'.");
    }
}