using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPcl = "input.pcl";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPcl))
        {
            Console.Error.WriteLine($"File not found: {inputPcl}");
            return;
        }

        // Load the PCL file (PCL is input‑only). Use PclLoadOptions as required.
        using (Document doc = new Document(inputPcl, new PclLoadOptions()))
        {
            // Ensure at least one page was loaded.
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("No pages were loaded from the PCL file.");
                return;
            }

            // Add a WatermarkAnnotation to the first page.
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 600);

            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                // Example properties – adjust as needed.
                Contents = "CONFIDENTIAL",
                Color    = Aspose.Pdf.Color.Red,
                Opacity  = 0.5
            };

            // Attach the annotation to the page.
            page.Annotations.Add(watermark);

            // Save the modified document. PCL cannot be saved, so we output PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermark annotation added and saved to '{outputPdf}'.");
    }
}