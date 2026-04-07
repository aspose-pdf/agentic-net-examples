using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page7.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        var converter = new PdfConverter();
        converter.BindPdf(inputPath);
        converter.StartPage = 7;
        converter.EndPage = 7;
        // Set DPI using a Resolution object (Aspose.Pdf.Devices)
        converter.Resolution = new Resolution(300);
        converter.DoConvert();

        if (converter.HasNextImage())
        {
            // Save image; format is inferred from the file extension (.png)
            converter.GetNextImage(outputPath);
            Console.WriteLine($"Page 7 saved as PNG: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("No image was generated for the specified page.");
        }

        converter.Close();
    }
}
