using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "inline_text_image.pdf";
        const string imagePath = "sample.png";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Build a paragraph‑like sequence that contains text fragments and an inline image.
            // Aspose.Pdf does not expose a "Paragraph" class; instead we add the fragments
            // directly to the page's Paragraphs collection. The fragments are rendered one after
            // another, which effectively gives an inline text‑image appearance.

            // Text before the image
            page.Paragraphs.Add(new TextFragment("This is inline image: "));

            // Inline image fragment – load the image via a stream (the Image class has no
            // constructor that accepts a file path).
            Image img = new Image();
            // Keep the stream open until the document is saved.
            FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            img.ImageStream = imgStream;
            img.FixWidth = 50;
            img.FixHeight = 50;
            page.Paragraphs.Add(img);

            // Text after the image
            page.Paragraphs.Add(new TextFragment(" after the image."));

            // Save the PDF
            doc.Save(outputPath);

            // Dispose the image stream after saving – the PDF has already consumed the data.
            imgStream.Dispose();
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
