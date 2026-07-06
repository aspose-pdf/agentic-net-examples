using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML content into a PDF document
        using (Document doc = new Document())
        {
            doc.BindXml(xmlPath);

            // Configure optimization options to downsample large images
            OptimizationOptions opt = new OptimizationOptions
            {
                // Limit image resolution (e.g., 150 DPI) to reduce size
                MaxResoultion = 150,
                // Compress PDF objects
                CompressObjects = true,
                // Remove objects and streams that are not used
                RemoveUnusedObjects = true,
                RemoveUnusedStreams = true,
                // Subset fonts to keep only used glyphs
                SubsetFonts = true
            };

            // Apply the optimization strategy to the document
            doc.OptimizeResources(opt);

            // Save the optimized PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPdf}'.");
    }
}