using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the XML and convert it to a PDF document
            using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
            {
                // Configure optimization options to downsample/compress large images
                OptimizationOptions opt = OptimizationOptions.All();
                opt.CompressImages = true;          // enable image compression
                opt.ImageQuality = 50;               // JPEG quality (0‑100), lower = higher compression
                opt.RemoveUnusedObjects = true;     // clean up unused PDF objects
                opt.RemoveUnusedStreams = true;     // clean up unused streams

                // Apply the optimization (includes down‑sampling where applicable)
                doc.OptimizeResources(opt);

                // Save the optimized PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"Optimized PDF saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
