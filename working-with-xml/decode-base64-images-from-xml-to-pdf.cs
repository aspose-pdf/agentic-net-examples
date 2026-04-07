using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for any text handling if needed

class Program
{
    static void Main()
    {
        const string xmlPath      = "input.xml";      // XML containing base64 images
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file into a PDF document.
        // XmlLoadOptions is required for XML → PDF conversion.
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Parse the XML to locate base64‑encoded image data.
            // This example assumes images are stored in <ImageData> elements.
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNodeList imageNodes = xmlDoc.GetElementsByTagName("ImageData");

            int pageNumber = 1; // start with first page (already created by load)
            foreach (XmlNode node in imageNodes)
            {
                // Decode the Base64 string.
                string base64 = node.InnerText.Trim();
                if (string.IsNullOrEmpty(base64))
                    continue;

                byte[] imageBytes;
                try
                {
                    imageBytes = Convert.FromBase64String(base64);
                }
                catch (FormatException)
                {
                    Console.Error.WriteLine("Invalid Base64 image data encountered; skipping.");
                    continue;
                }

                // Create a new page for each image (optional: reuse existing page).
                pdfDoc.Pages.Add();
                Page page = pdfDoc.Pages[pageNumber];

                // Create an Aspose.Pdf.Image and assign the decoded stream.
                Image img = new Image
                {
                    ImageStream = new MemoryStream(imageBytes)
                };

                // Optionally set dimensions (here we let the image keep its original size).
                // img.FixWidth = 200;   // example width in points
                // img.FixHeight = 150;  // example height in points

                // Add the image to the page.
                page.Paragraphs.Add(img);

                pageNumber++;
            }

            // Save the resulting PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with embedded images saved to '{outputPdfPath}'.");
    }
}