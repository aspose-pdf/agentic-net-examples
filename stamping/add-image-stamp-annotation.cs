using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the sample PDF and stamp image
        string inputPath = "input.pdf";
        string imagePath = "stamp.png";

        // Create a simple PNG image that will be used as a stamp
        CreateSampleImage(imagePath);

        // -----------------------------------------------------------------
        // Step 1: Create a sample PDF and add a text annotation (existing)
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            TextAnnotation textAnnot = new TextAnnotation(page, textRect);
            textAnnot.Contents = "Sample Text Annotation";
            page.Annotations.Add(textAnnot);
            doc.Save(inputPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Re‑open the PDF and add an image stamp annotation
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];

            // Load the image bytes and keep the stream open until the document is saved
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            MemoryStream imageStream = new MemoryStream(imageBytes);

            // Create the stamp annotation and assign the image stream
            Aspose.Pdf.Rectangle stampRect = new Aspose.Pdf.Rectangle(300, 500, 400, 600);
            StampAnnotation stampAnnot = new StampAnnotation(page, stampRect);
            stampAnnot.Image = imageStream; // Image property expects a Stream
            stampAnnot.Opacity = 0.7f; // optional transparency

            // Add the stamp annotation while preserving existing ones
            page.Annotations.Add(stampAnnot);

            doc.Save("output.pdf");

            // Dispose the image stream after the PDF has been written
            imageStream.Dispose();
        }
    }

    private static void CreateSampleImage(string path)
    {
        // Generate a 100x100 red square PNG using System.Drawing
        using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(100, 100))
        {
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.Red);
            }
            bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}