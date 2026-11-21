using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_figure_annotation.pdf";
        const string imagePath = "figure.png"; // external image to use as appearance

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Keep streams alive until the document is saved
        List<MemoryStream> imageStreams = new List<MemoryStream>();

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury) in points
            // Here we place a 200x200 box at position (100,500)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a StampAnnotation which can use an external image as its appearance
            StampAnnotation stamp = new StampAnnotation(page, rect);

            // Load the external image into a MemoryStream and assign it to the annotation
            byte[] imgBytes = File.ReadAllBytes(imagePath);
            MemoryStream imgStream = new MemoryStream(imgBytes);
            // Keep a reference so the stream is not disposed before saving
            imageStreams.Add(imgStream);
            stamp.Image = imgStream; // Image property expects a Stream

            // Optional: set a tooltip (Contents) and a title (Title) for the annotation
            stamp.Title = "Figure Annotation";
            stamp.Contents = "This annotation shows an external image as its appearance.";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(stamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with figure annotation: {outputPdf}");
    }
}
