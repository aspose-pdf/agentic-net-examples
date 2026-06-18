using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document containing Base64‑encoded images
        XDocument xdoc = XDocument.Load(xmlPath);

        // Assume each image is stored in an <Image> element
        var imageElements = xdoc.Descendants("Image");

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document())
        {
            int pageIndex = 1;

            foreach (var imgElem in imageElements)
            {
                string base64 = imgElem.Value.Trim();
                if (string.IsNullOrEmpty(base64))
                    continue;

                // Decode Base64 string to raw image bytes
                byte[] imgBytes;
                try
                {
                    imgBytes = Convert.FromBase64String(base64);
                }
                catch (FormatException)
                {
                    Console.Error.WriteLine("Invalid Base64 image data encountered.");
                    continue;
                }

                // Ensure a page exists for this image
                if (pdfDoc.Pages.Count < pageIndex)
                    pdfDoc.Pages.Add();

                Page page = pdfDoc.Pages[pageIndex];

                // Define a rectangle that covers the whole page
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,                                   // left
                    0,                                   // bottom
                    page.PageInfo.Width,                 // right
                    page.PageInfo.Height);               // top

                // Add the image to the page, centering it within the rectangle
                using (MemoryStream ms = new MemoryStream(imgBytes))
                {
                    page.AddImage(ms, rect);
                }

                pageIndex++;
            }

            // Save the resulting PDF (lifecycle rule: use Document.Save)
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created successfully at '{pdfPath}'.");
    }
}