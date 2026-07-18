using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "optimized_output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load XML and create a PDF document from it
            using (Document doc = new Document())
            {
                doc.BindXml(xmlPath);

                // Set up optimization options: downsample images with resolution higher than 150 DPI
                OptimizationOptions opt = new OptimizationOptions
                {
                    MaxResoultion = 150 // downsample threshold (DPI)
                };

                // Apply the optimization (downsampling + resource cleanup)
                doc.OptimizeResources(opt);

                // Save the optimized PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Optimized PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}