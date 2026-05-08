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

        // Load the XML document that contains Base64‑encoded images.
        XDocument xDoc = XDocument.Load(xmlPath);

        // Create an empty PDF document.
        using (Document pdfDoc = new Document())
        {
            // Add a single page where images will be placed.
            pdfDoc.Pages.Add();
            Page page = pdfDoc.Pages[1];

            int imageCount = 0;
            // Assume each image is stored in an <Image> element.
            foreach (var imgElem in xDoc.Descendants("Image"))
            {
                string base64 = imgElem.Value.Trim();
                if (string.IsNullOrEmpty(base64))
                    continue;

                byte[] imgBytes;
                try
                {
                    imgBytes = Convert.FromBase64String(base64);
                }
                catch (FormatException)
                {
                    Console.Error.WriteLine("Invalid Base64 data encountered; skipping element.");
                    continue;
                }

                // Define a rectangle for the image placement.
                // Position images vertically with a simple offset.
                double yTop = 750 - imageCount * 200; // adjust as needed
                double yBottom = yTop - 150;
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, yBottom, 550, yTop);

                // Add the image to the page.
                using (MemoryStream ms = new MemoryStream(imgBytes))
                {
                    page.AddImage(ms, rect);
                }

                imageCount++;
            }

            // Save the resulting PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created successfully at '{pdfPath}'.");
    }
}