using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string watermarkImgPath = "watermark.png";
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkImgPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImgPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Process each page (you can limit to specific pages if needed)
            foreach (Page page in doc.Pages)
            {
                // Ensure the page has a Layers collection
                if (page.Layers == null)
                    page.Layers = new List<Layer>();

                // Add the watermark image to the page resources and obtain its name
                string imageName;
                using (FileStream imgStream = File.OpenRead(watermarkImgPath))
                {
                    // Add the image stream to the page's image collection
                    imageName = page.Resources.Images.Add(imgStream);
                }

                // Define the rectangle where the watermark will be placed
                // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

                // Build a transformation matrix that maps the image to the rectangle
                // Matrix: [ a b 0 d e f ] where a = width, d = height, e = llx, f = lly
                double[] matrixValues = new double[]
                {
                    rect.URX - rect.LLX, 0,               // a, b
                    0, rect.URY - rect.LLY,               // c, d (c is 0 for axis‑aligned)
                    rect.LLX, rect.LLY                    // e, f (translation)
                };
                Matrix placementMatrix = new Matrix(matrixValues);

                // Create a new optional content group (layer) for the watermark
                Layer watermarkLayer = new Layer("WatermarkLayer", "WatermarkOCG");

                // Populate the layer's content stream with PDF operators
                watermarkLayer.Contents.Add(new GSave());                     // Save graphics state
                watermarkLayer.Contents.Add(new ConcatenateMatrix(placementMatrix)); // Apply transformation
                watermarkLayer.Contents.Add(new Do(imageName));               // Paint the image
                watermarkLayer.Contents.Add(new GRestore());                  // Restore graphics state

                // Attach the layer to the current page
                page.Layers.Add(watermarkLayer);

                // Merge all layers on this page into a single layer (optional)
                // The new layer will contain both the original content and the watermark
                page.MergeLayers("MergedLayer");
            }

            // Save the modified document
            doc.Save(outputPdfPath);
            Console.WriteLine($"PDF with watermark layer saved to '{outputPdfPath}'.");
        }
    }
}