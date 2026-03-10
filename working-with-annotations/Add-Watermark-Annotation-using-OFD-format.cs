using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputOfdPath  = "input.ofd";
        const string outputPdfPath = "watermarked_output.pdf";

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputOfdPath}");
            return;
        }

        // Load the OFD file (OFD is input‑only; we will save the result as PDF)
        using (Document doc = new Document(inputOfdPath, new OfdLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Create a rectangle for the watermark annotation (coordinates are in points)
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 550);

            // Create the watermark annotation on the first page
            WatermarkAnnotation watermark = new WatermarkAnnotation(doc.Pages[1], rect)
            {
                // Example properties – adjust as needed
                Color   = Aspose.Pdf.Color.LightGray,
                Opacity = 0.5,
                Contents = "CONFIDENTIAL"
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(watermark);

            // Save the modified document as PDF (OFD cannot be saved)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}