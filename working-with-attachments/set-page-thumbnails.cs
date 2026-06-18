using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Devices;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with three pages (evaluation mode limit)
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the PDF and add a thumbnail image to each page using the Drawing API
        using (Document doc = new Document("input.pdf"))
        {
            int pageCount = doc.Pages.Count;
            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                // Generate a thumbnail for the current page
                using (MemoryStream thumbStream = new MemoryStream())
                {
                    ThumbnailDevice thumbDevice = new ThumbnailDevice(100, 100);
                    thumbDevice.Process(doc.Pages[pageIndex], thumbStream);
                    thumbStream.Position = 0;

                    // Save the thumbnail to a temporary file
                    string thumbFileName = "thumb_page" + pageIndex + ".png";
                    File.WriteAllBytes(thumbFileName, thumbStream.ToArray());

                    // Create an Image object that points to the thumbnail file
                    Image thumbnailImage = new Image();
                    thumbnailImage.File = thumbFileName;

                    // Optionally define a rectangle for positioning (left, bottom, width, height)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(10f, 10f, 100f, 100f);
                    // Add the image to the page; the rectangle can be used via Image.FixWidth/FixHeight if needed
                    doc.Pages[pageIndex].Paragraphs.Add(thumbnailImage);
                }
            }

            doc.Save("output.pdf");
        }
    }
}