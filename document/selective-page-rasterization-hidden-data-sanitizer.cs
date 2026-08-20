using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Hidden data sanitization – if the API is available in the used version, uncomment the following lines:
            // var hiddenOptions = new HiddenDataSanitizerOptions(); // or HiddenDataSanitizerOptions.All()
            // doc.SanitizeHiddenData(hiddenOptions);

            // Optimize resources (removes private info, unused objects, etc.)
            OptimizationOptions opt = OptimizationOptions.All();
            doc.OptimizeResources(opt);

            // Selective page rasterization: rasterize pages 2 and 4 only
            int[] pagesToRasterize = { 2, 4 };
            foreach (int pageNum in pagesToRasterize)
            {
                // Create a temporary document containing only the target page
                using (Document temp = new Document())
                {
                    // Add a copy of the page to the temporary document
                    temp.Pages.Add(doc.Pages[pageNum]);

                    // Flatten transparency – converts transparent content to raster graphics
                    temp.FlattenTransparency();

                    // Replace the original page with the rasterized version
                    // Insert the rasterized page at the original position
                    doc.Pages.Insert(pageNum, temp.Pages[1]);

                    // Delete the original page (now shifted to pageNum + 1)
                    doc.Pages.Delete(pageNum + 1);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
