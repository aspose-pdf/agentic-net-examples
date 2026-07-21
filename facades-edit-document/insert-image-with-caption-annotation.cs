using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string imagePath  = "image.jpg";
        const string caption    = "Sample Caption";

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Input PDF or image file not found.");
            return;
        }

        // ---------- Add image as a stamp ----------
        // PdfFileStamp works with the Facades API and does not require a using block.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp, bind the image, and set its position.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);               // set the image to be stamped
        stamp.SetOrigin(50, 150);                 // lower‑left corner of the image
        stamp.SetImageSize(100, 100);             // width and height of the image
        stamp.Opacity = 1.0f;                     // fully opaque

        // Add the stamp to the PDF.
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        // ---------- Add caption underneath the image ----------
        // Open the PDF that now contains the image stamp.
        using (Document doc = new Document(outputPdf))
        {
            // Assume we work with the first page.
            Aspose.Pdf.Page page = doc.Pages[1];

            // Define a rectangle for the caption text.
            // Position it slightly below the image (y = 130).
            Aspose.Pdf.Rectangle captionRect = new Aspose.Pdf.Rectangle(50, 130, 150, 150);

            // Create a text annotation with the caption.
            TextAnnotation txtAnn = new TextAnnotation(page, captionRect)
            {
                Contents = caption,
                Color    = Aspose.Pdf.Color.Transparent, // no border color
                Opacity  = 0.0f                         // make the annotation background invisible
            };

            // Add the annotation to the page.
            page.Annotations.Add(txtAnn);

            // Save the final PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image with caption added and saved to '{outputPdf}'.");
    }
}