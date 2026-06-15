using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace AddImageStampFormExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with a single text box form field.
            using (Document sampleDoc = new Document())
            {
                Page samplePage = sampleDoc.Pages.Add();
                // Define the rectangle where the form field will appear.
                Rectangle fieldRect = new Rectangle(100, 600, 300, 650);
                TextBoxField textBox = new TextBoxField(samplePage, fieldRect);
                textBox.PartialName = "SampleField";
                // Add the field to the form on page 1.
                sampleDoc.Form.Add(textBox, 1);
                // Save the PDF that will be used as input.
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Create a tiny placeholder image (logo.png) to be used as a stamp.
            // This is a 1x1 pixel transparent PNG encoded in Base64.
            string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=";
            byte[] pngBytes = Convert.FromBase64String(base64Png);
            File.WriteAllBytes("logo.png", pngBytes);

            // Step 3: Open the PDF, add an image stamp to each page, and keep form fields functional.
            using (Document doc = new Document("input.pdf"))
            {
                // Iterate through all pages (only one in this example).
                foreach (Page page in doc.Pages)
                {
                    ImageStamp stamp = new ImageStamp("logo.png");
                    stamp.Width = 100;
                    stamp.Height = 50;
                    stamp.HorizontalAlignment = HorizontalAlignment.Right;
                    stamp.VerticalAlignment = VerticalAlignment.Bottom;
                    stamp.Opacity = 0.5f;
                    // Add the stamp to the current page.
                    page.AddStamp(stamp);
                }

                // Save the resulting PDF with the stamp applied.
                doc.Save("output.pdf");
            }
        }
    }
}