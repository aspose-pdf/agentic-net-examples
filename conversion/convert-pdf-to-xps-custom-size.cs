using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.xps";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document pdfDoc = new Document(inputPath))
        {
            // Apply custom page size (595x420 points) to achieve landscape orientation for every page
            foreach (Page page in pdfDoc.Pages)
            {
                page.PageInfo.Width = 595;   // width in points (greater than height => landscape)
                page.PageInfo.Height = 420;  // height in points
                // No Orientation property exists on PageInfo; orientation is implied by width/height values.
            }

            XpsSaveOptions saveOptions = new XpsSaveOptions();
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS with custom size: {outputPath}");
    }
}
