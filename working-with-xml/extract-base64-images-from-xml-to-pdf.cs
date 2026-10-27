using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Image class lives here

class Program
{
    static void Main()
    {
        const string xmlPath = "images.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document containing base64‑encoded images
        XDocument xdoc = XDocument.Load(xmlPath);
        // Assume each image is stored in an <ImageData> element
        var imageElements = xdoc.Descendants("ImageData");

        // Keep the memory streams alive until the PDF is saved
        List<MemoryStream> streams = new List<MemoryStream>();

        // Create a new PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document())
        {
            int pageIndex = 0; // will be incremented to match 1‑based page indexing

            foreach (var elem in imageElements)
            {
                string base64 = elem.Value.Trim();
                if (string.IsNullOrEmpty(base64))
                    continue; // skip empty entries

                // Decode the base64 string to a byte array
                byte[] imgBytes;
                try
                {
                    imgBytes = Convert.FromBase64String(base64);
                }
                catch (FormatException)
                {
                    Console.Error.WriteLine("Invalid base64 string encountered; skipping this entry.");
                    continue;
                }

                // Add a new page for the image
                pdfDoc.Pages.Add();
                pageIndex++; // Aspose.Pdf uses 1‑based page indexing

                // Create a MemoryStream for the image data and keep it in the list
                MemoryStream ms = new MemoryStream(imgBytes);
                streams.Add(ms);

                // Instantiate Image using the parameter‑less constructor and assign the stream
                Image img = new Image
                {
                    ImageStream = ms,
                    // Optional: scale the image to fit the page
                    // FixWidth = pdfDoc.Pages[pageIndex].PageInfo.Width,
                    // FixHeight = pdfDoc.Pages[pageIndex].PageInfo.Height
                };

                // Add the image to the page's paragraph collection
                pdfDoc.Pages[pageIndex].Paragraphs.Add(img);
            }

            // Save the resulting PDF (no SaveOptions needed for PDF output)
            pdfDoc.Save(pdfPath);
        }

        // Dispose all streams after the document has been saved
        foreach (var s in streams)
            s.Dispose();

        Console.WriteLine($"PDF successfully created at '{pdfPath}'.");
    }
}
