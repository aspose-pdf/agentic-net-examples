using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string xmlPath      = "input.xml";      // XML containing base64 images
        const string outputPdf    = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file (no special load options required for plain XML)
        XDocument xDoc = XDocument.Load(xmlPath);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Iterate over each element that holds a base64‑encoded image.
            // Adjust the element name/path according to your XML schema.
            foreach (XElement imgElem in xDoc.Descendants("Image"))
            {
                string base64 = imgElem.Value.Trim();
                if (string.IsNullOrEmpty(base64))
                    continue;

                // Decode the base64 string to a byte array
                byte[] imgBytes;
                try
                {
                    imgBytes = Convert.FromBase64String(base64);
                }
                catch (FormatException)
                {
                    Console.Error.WriteLine("Invalid base64 data encountered; skipping element.");
                    continue;
                }

                // Add a new page for this image
                Page page = pdfDoc.Pages.Add();

                // Define the rectangle that covers the whole page.
                // Aspose.Pdf.Rectangle constructor: (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,                                   // lower‑left X
                    0,                                   // lower‑left Y
                    page.PageInfo.Width,                 // upper‑right X (page width)
                    page.PageInfo.Height);               // upper‑right Y (page height)

                // Insert the image using a memory stream.
                using (MemoryStream imgStream = new MemoryStream(imgBytes))
                {
                    page.AddImage(imgStream, rect);
                }
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}