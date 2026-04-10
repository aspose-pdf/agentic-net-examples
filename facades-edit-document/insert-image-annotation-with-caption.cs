using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string imagePath      = "image.jpg";
        const string captionText    = "Sample Caption";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation (lower‑left (50,150), upper‑right (150,250))
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(50, 150, 150, 250);

            // Create a StampAnnotation on the selected page
            StampAnnotation stamp = new StampAnnotation(page, annotRect);

            // Load the image into a memory stream and assign it to the annotation
            byte[] imgBytes = File.ReadAllBytes(imagePath);
            stamp.Image = new MemoryStream(imgBytes);

            // Set the caption (appears as the annotation's tooltip / popup)
            stamp.Contents = captionText;

            // NOTE: StampAnnotation does not expose a CaptionPosition property in the current
            // Aspose.Pdf version. The caption is stored in the Contents field and is shown as a
            // tooltip/popup when the user hovers or clicks the annotation. If a visual caption
            // beneath the image is required, consider adding a separate TextFragment annotation.

            // Add the annotation to the page
            page.Annotations.Add(stamp);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image annotation with caption saved to '{outputPdfPath}'.");
    }
}
