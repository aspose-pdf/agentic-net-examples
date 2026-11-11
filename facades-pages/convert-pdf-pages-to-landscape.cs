using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // A4 portrait size (points). Landscape is obtained by swapping width/height.
            var a4 = PageSize.A4;
            double landscapeWidth = a4.Height;   // width becomes original height
            double landscapeHeight = a4.Width;   // height becomes original width

            // Apply the landscape size to every page via PageInfo
            foreach (Page page in doc.Pages)
            {
                page.PageInfo.Width = landscapeWidth;
                page.PageInfo.Height = landscapeHeight;
                // Optional: explicitly mark the page as landscape
                page.PageInfo.IsLandscape = true;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Landscape PDF saved to '{outputPath}'.");
    }
}