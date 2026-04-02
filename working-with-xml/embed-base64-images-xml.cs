using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML file containing <Image> elements with Base64 data
        string xmlPath = "input.xml";
        // Output PDF file
        string pdfPath = "output.pdf";

        // Load the XML document – if the file does not exist, create an empty document to avoid a runtime exception
        XDocument xdoc;
        if (File.Exists(xmlPath))
        {
            xdoc = XDocument.Load(xmlPath);
        }
        else
        {
            Console.WriteLine($"Warning: '{xmlPath}' not found. An empty PDF will be generated.");
            xdoc = new XDocument(new XElement("Images"));
        }

        // Create a new PDF document
        using (Document pdfDocument = new Document())
        {
            // Add a single page where images will be placed
            pdfDocument.Pages.Add();

            // Keep streams alive until the PDF is saved
            List<MemoryStream> imageStreams = new List<MemoryStream>();

            // Iterate over each <Image> element in the XML
            foreach (XElement imgElem in xdoc.Descendants("Image"))
            {
                string base64 = imgElem.Value.Trim();
                if (String.IsNullOrEmpty(base64))
                {
                    continue;
                }

                // Decode Base64 string to byte array
                byte[] imageBytes = Convert.FromBase64String(base64);

                // Create a memory stream for the image data
                MemoryStream ms = new MemoryStream(imageBytes);
                imageStreams.Add(ms);

                // Create an Aspose.Pdf.Image and assign the stream
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                pdfImage.ImageStream = ms;

                // Add the image to the first PDF page (index 1)
                pdfDocument.Pages[1].Paragraphs.Add(pdfImage);
            }

            // Save the PDF document
            pdfDocument.Save(pdfPath);

            // Dispose all memory streams
            foreach (MemoryStream ms in imageStreams)
            {
                ms.Dispose();
            }
        }
    }
}
