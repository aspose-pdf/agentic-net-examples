using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string xmlPath    = "input.xml";      // XML that references images
        const string outputPath = "optimized.pdf";   // Resulting PDF

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load XML into a PDF document
            using (Document doc = new Document())
            {
                doc.BindXml(xmlPath);

                // Configure optimization: downsample images above the given DPI
                OptimizationOptions opt = new OptimizationOptions
                {
                    // Maximum resolution (dpi) for images; higher resolutions will be scaled down
                    MaxResoultion = 150
                };

                // Apply the optimization to the document
                doc.OptimizeResources(opt);

                // Save the optimized PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}