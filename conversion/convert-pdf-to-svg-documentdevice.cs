using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "output_svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the custom output folder exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (using the recommended using block)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure SVG save options – classes are in the Aspose.Pdf namespace
            var svgOptions = new SvgSaveOptions();
            // Optional: set any specific SVG options here, e.g., svgOptions.PageSize = PageSize.A4;

            // Save each page as an individual SVG file inside the output folder
            pdfDoc.Save(outputFolder, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG files in '{outputFolder}'.");
    }
}