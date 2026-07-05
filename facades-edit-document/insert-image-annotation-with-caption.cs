using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.jpg";
        const string caption = "Sample caption";

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Open the existing PDF
        using (Document doc = new Document(inputPdf))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Rectangle for the image annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle imgRect = new Aspose.Pdf.Rectangle(50, 150, 150, 250);

            // Create a stamp annotation that will hold the image
            StampAnnotation imgAnnot = new StampAnnotation(page, imgRect);

            // Load the image bytes and assign a stream to the annotation (StampAnnotation.Image expects a Stream)
            byte[] imgBytes = File.ReadAllBytes(imagePath);
            imgAnnot.Image = new MemoryStream(imgBytes);

            // Set the caption (will appear in the annotation popup)
            imgAnnot.Contents = caption;

            // Optional visual styling
            imgAnnot.Color = Aspose.Pdf.Color.Black;

            // Add the image annotation to the page
            page.Annotations.Add(imgAnnot);

            // Add a visible text annotation underneath the image as a caption
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(50, 130, 150, 150);
            TextAnnotation txtAnnot = new TextAnnotation(page, txtRect)
            {
                Title = "Caption",
                Contents = caption,
                Color = Aspose.Pdf.Color.LightGray,
                Open = true
            };
            page.Annotations.Add(txtAnnot);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image annotation with caption saved to '{outputPdf}'.");
    }
}
