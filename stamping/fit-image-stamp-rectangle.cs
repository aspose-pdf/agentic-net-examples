using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF file
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Create a sample image to be used as a stamp
        using (Bitmap bitmap = new Bitmap(200, 200))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(System.Drawing.Color.Red);
            }
            bitmap.Save("logo.png", ImageFormat.Png);
        }

        // Open the PDF and add the image stamp within a defined rectangle
        using (Document pdfDoc = new Document("input.pdf"))
        {
            // Define the rectangle (left, bottom, width, height) where the stamp should fit
            // Aspose.Pdf.Rectangle constructor expects (llx, lly, urx, ury)
            float left = 100f;
            float bottom = 500f;
            float width = 200f;
            float height = 200f;
            Aspose.Pdf.Rectangle stampRect = new Aspose.Pdf.Rectangle(left, bottom, left + width, bottom + height);

            // Create the image stamp from a file stream
            using (FileStream imageStream = new FileStream("logo.png", FileMode.Open, FileAccess.Read))
            {
                ImageStamp imgStamp = new ImageStamp(imageStream);
                // Adjust stamp size to match the rectangle size
                imgStamp.Width = stampRect.Width;
                imgStamp.Height = stampRect.Height;
                // Position the stamp at the rectangle's lower‑left corner
                imgStamp.XIndent = stampRect.LLX;
                imgStamp.YIndent = stampRect.LLY;
                // Optional: set opacity
                imgStamp.Opacity = 0.8f;

                // Add the stamp to the first page
                pdfDoc.Pages[1].AddStamp(imgStamp);
            }

            pdfDoc.Save("output.pdf");
        }
    }
}
