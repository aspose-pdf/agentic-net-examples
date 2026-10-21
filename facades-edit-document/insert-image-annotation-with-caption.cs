using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";
        const string imageFile  = "picture.jpg";
        const string outputPdf  = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imageFile))
        {
            Console.Error.WriteLine($"Image file not found: {imageFile}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);
        // Work with the first page (1‑based index)
        Page page = pdfDocument.Pages[1];

        // -----------------------------------------------------------------
        // 1) Add the image to the page.
        //    Rectangle constructor expects (llx, lly, urx, ury).
        //    Here we place the lower‑left corner at (50,150) and give the
        //    image a width of 150 points and a height of 100 points.
        // -----------------------------------------------------------------
        var imageRect = new Rectangle(50, 150, 200, 250); // 50+150 = 200, 150+100 = 250
        using (FileStream imgStream = File.OpenRead(imageFile))
        {
            page.AddImage(imgStream, imageRect);
        }

        // -----------------------------------------------------------------
        // 2) Add a caption (text annotation) underneath the image.
        // -----------------------------------------------------------------
        var captionRect = new Rectangle(50, 130, 200, 145); // 20 points below image, height ~15
        var captionAnnotation = new TextAnnotation(page, captionRect)
        {
            Contents = "Sample caption text",
            Title    = "Caption"
        };
        page.Annotations.Add(captionAnnotation);

        // Save the modified PDF
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Image annotation with caption saved to '{outputPdf}'.");
    }
}
