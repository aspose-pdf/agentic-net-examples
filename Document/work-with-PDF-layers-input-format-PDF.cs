using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class PdfLayerDemo
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_layer.pdf";
        const string layerPdf  = "extracted_layer.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Create a new layer and add it to the page
            // ------------------------------------------------------------
            // Constructor: Layer(string name, string optionalContentGroupId)
            Layer myLayer = new Layer("MyLayer", "OCG1");
            page.Layers.Add(myLayer); // Add the layer to the page's layer collection

            // ------------------------------------------------------------
            // 2. Lock the layer (prevents further modifications)
            // ------------------------------------------------------------
            myLayer.Lock();

            // ------------------------------------------------------------
            // 3. Unlock the layer (required before flattening or deleting)
            // ------------------------------------------------------------
            myLayer.Unlock();

            // ------------------------------------------------------------
            // 4. Flatten the layer (remove optional content group markers)
            //    Parameter 'cleanupContentStream' set to true removes the OCG markers.
            // ------------------------------------------------------------
            myLayer.Flatten(true);

            // ------------------------------------------------------------
            // 5. Save the current layer as a separate PDF file
            // ------------------------------------------------------------
            myLayer.Save(layerPdf);

            // ------------------------------------------------------------
            // 6. Delete the layer from the document
            // ------------------------------------------------------------
            myLayer.Delete();

            // ------------------------------------------------------------
            // 7. Merge all remaining layers on the page into a single layer
            // ------------------------------------------------------------
            page.MergeLayers("MergedLayer");

            // ------------------------------------------------------------
            // 8. Save the modified document
            // ------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine("Layer operations completed successfully.");
    }
}