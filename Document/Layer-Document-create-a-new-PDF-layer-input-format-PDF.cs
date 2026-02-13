using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_layer.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPdf);

            // Create a new layer (name, description)
            Layer newLayer = new Layer("MyLayer", "Sample layer created via Aspose.Pdf");

            // Add the layer to the first page (pages are 1‑based)
            Page firstPage = pdfDocument.Pages[1];
            firstPage.Layers.Add(newLayer);

            // Optionally, add a simple text annotation to the layer to demonstrate visibility
            // (The annotation itself is not part of the layer, but this shows the page is still usable)
            var rect = new Rectangle(100, 500, 300, 550);
            var textAnnot = new TextAnnotation(firstPage, rect)
            {
                Contents = "Layer added",
                Color = Color.Blue
            };
            firstPage.Annotations.Add(textAnnot);

            // Save the modified PDF
            pdfDocument.Save(outputPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}