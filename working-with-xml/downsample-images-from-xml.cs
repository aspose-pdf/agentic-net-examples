using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string xmlPath      = "input.xml";   // XML that references images
        const string outputPdf    = "optimized.pdf";
        const int   maxResolution = 150; // DPI – images above this will be down‑sampled

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the XML and create a PDF document from it
            using (Document doc = new Document())
            {
                doc.BindXml(xmlPath); // XML → PDF

                // Configure optimization: down‑sample images exceeding maxResolution DPI
                OptimizationOptions opt = new OptimizationOptions();
                opt.MaxResoultion = maxResolution; // note: property name is MaxResoultion (typo in API)

                // Apply the optimization (removes unused resources, merges duplicates, down‑samples)
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