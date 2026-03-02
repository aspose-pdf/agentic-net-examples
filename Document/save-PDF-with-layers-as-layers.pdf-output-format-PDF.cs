using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "layers.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a new layer (name, optional content group ID)
            Layer layer = new Layer("SampleLayer", "OCG1");

            // Add the layer to the page's layer collection
            page.Layers.Add(layer);

            // Save the document as a PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with layers saved to '{outputPath}'.");
    }
}