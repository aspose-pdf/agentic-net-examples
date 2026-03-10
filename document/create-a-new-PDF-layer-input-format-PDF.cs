using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_layer.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Create a new layer: first argument is the layer name,
            // second argument is the optional content group identifier.
            Layer newLayer = new Layer("MyNewLayer", "OCG1");

            // Add the layer to the first page's layer collection.
            // Page.Layers is a List<Layer>, so Add() is appropriate.
            doc.Pages[1].Layers.Add(newLayer);

            // Save the modified PDF (standard Document.Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new layer at '{outputPath}'.");
    }
}