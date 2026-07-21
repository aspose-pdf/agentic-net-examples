using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXps = "output.xps";

        // Desired page size: 8.5 x 11 inches (points: 1 inch = 72 points)
        double width  = 8.5 * 72; // 612 points
        double height = 11  * 72; // 792 points

        // Set orientation: true = landscape, false = portrait
        bool landscape = true;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Apply custom size and orientation to every page
            foreach (Page page in doc.Pages)
            {
                if (landscape)
                {
                    // Swap dimensions for landscape orientation
                    page.SetPageSize(height, width);
                    page.PageInfo.IsLandscape = true;
                }
                else
                {
                    page.SetPageSize(width, height);
                    page.PageInfo.IsLandscape = false;
                }
            }

            // Save as XPS using explicit XpsSaveOptions
            XpsSaveOptions saveOptions = new XpsSaveOptions();
            doc.Save(outputXps, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: {outputXps}");
    }
}