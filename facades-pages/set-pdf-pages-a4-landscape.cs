using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Apply A4 landscape dimensions to every page
            // A4 size is 297 x 210 mm; in points (1 pt = 1/72 inch) Aspose.Pdf provides the values via PageSize.A4
            // For landscape we swap width and height.
            double landscapeWidth  = PageSize.A4.Height; // 210 mm in points
            double landscapeHeight = PageSize.A4.Width;  // 297 mm in points

            foreach (Page page in doc.Pages)
            {
                page.PageInfo.Width       = landscapeWidth;
                page.PageInfo.Height      = landscapeHeight;
                page.PageInfo.IsLandscape = true;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved landscape PDF to '{outputPath}'.");
    }
}