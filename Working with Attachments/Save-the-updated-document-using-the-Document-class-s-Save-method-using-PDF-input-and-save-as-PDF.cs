using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the destination PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // OPTIONAL: make a simple modification (add a text annotation on page 1)
            // Page indexing in Aspose.Pdf is 1‑based.
            Page page = doc.Pages[1];

            // Fully qualified rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation and configure its properties
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Sample Note",
                Contents = "This is a text annotation added programmatically.",
                Color    = Aspose.Pdf.Color.Yellow, // Use Aspose.Pdf.Color (cross‑platform)
                Open     = true,
                Icon     = TextIcon.Note
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(txtAnn);

            // Save the updated document as PDF.
            // Document.Save(string) without SaveOptions always writes PDF,
            // regardless of the file extension.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved successfully to '{outputPath}'.");
    }
}