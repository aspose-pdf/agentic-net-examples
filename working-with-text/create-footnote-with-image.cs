using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string outputPath = "footnote_with_image.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a text fragment that will contain the footnote reference
            TextFragment fragment = new TextFragment("This is a paragraph with a footnote reference.");

            // Create the footnote (Note) and set its textual content
            Note footNote = new Note();
            footNote.Text = "This is the footnote text.";

            // ------------------------------------------------------------
            // Create an in‑memory PNG image and add it to the footnote
            // ------------------------------------------------------------
            // 1. Build a simple bitmap (you can replace this with any image you like)
            Bitmap bitmap = new Bitmap(100, 50);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(System.Drawing.Color.LightGray);
                g.DrawString("Img", new System.Drawing.Font("Arial", 12), Brushes.Black, new PointF(10, 15));
            }

            // 2. Save the bitmap to a MemoryStream as PNG
            MemoryStream imageStream = new MemoryStream();
            bitmap.Save(imageStream, ImageFormat.Png);
            // Reset the stream position so Aspose.Pdf can read from the beginning
            imageStream.Position = 0;

            // 3. Create an Aspose.Pdf.Image and assign the stream
            Aspose.Pdf.Image footnoteImage = new Aspose.Pdf.Image();
            footnoteImage.ImageStream = imageStream;

            // 4. Add the image to the footnote's Paragraphs collection
            footNote.Paragraphs.Add(footnoteImage);

            // Associate the footnote with the text fragment
            fragment.FootNote = footNote;

            // Add the text fragment (with footnote) to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF document
            doc.Save(outputPath);

            // Clean‑up resources that are not covered by the using‑statement above
            imageStream.Dispose();
            bitmap.Dispose();
        }

        Console.WriteLine($"PDF with footnote saved to '{outputPath}'.");
    }
}
