using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "image.png";
        const string caption = "Sample Caption";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];

            // Define the rectangle where the image will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(50, 150, 150, 250);
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                page.AddImage(imgStream, imageRect);
            }

            // Define a rectangle for the caption placed just below the image
            Aspose.Pdf.Rectangle captionRect = new Aspose.Pdf.Rectangle(50, 130, 150, 150);
            TextAnnotation captionAnn = new TextAnnotation(page, captionRect);
            captionAnn.Contents = caption;
            captionAnn.Title = "Caption";
            captionAnn.Color = Aspose.Pdf.Color.Yellow; // optional visual styling
            page.Annotations.Add(captionAnn);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Image with caption annotation saved to '{outputPath}'.");
    }
}