using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for Aspose.Pdf types like Rectangle

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";   // XML containing base64‑encoded images
        const string outputPdf = "output.pdf";  // Resulting PDF file

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML as a PDF document (creates an empty PDF structure)
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Parse the XML to locate elements that hold base64 image data.
            // Adjust the element name ("ImageData") to match your XML schema.
            XDocument xdoc = XDocument.Load(xmlPath);
            foreach (var imgElem in xdoc.Descendants("ImageData"))
            {
                string base64 = imgElem.Value.Trim();
                if (string.IsNullOrEmpty(base64))
                    continue; // skip empty nodes

                byte[] imgBytes;
                try
                {
                    imgBytes = Convert.FromBase64String(base64);
                }
                catch (FormatException)
                {
                    Console.Error.WriteLine("Invalid Base64 string encountered; skipping element.");
                    continue;
                }

                // Add a new page for each decoded image.
                pdfDoc.Pages.Add();
                Page page = pdfDoc.Pages[pdfDoc.Pages.Count];

                // Define a rectangle that covers the whole page.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,                                   // lower‑left X
                    0,                                   // lower‑left Y
                    page.PageInfo.Width,                 // upper‑right X
                    page.PageInfo.Height);               // upper‑right Y

                // Insert the image, preserving its aspect ratio.
                using (MemoryStream ms = new MemoryStream(imgBytes))
                {
                    page.AddImage(ms, rect);
                }
            }

            // Save the final PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}