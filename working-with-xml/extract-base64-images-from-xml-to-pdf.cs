using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;               // Required for text‑related types (not used here but safe)

class EmbedBase64Images
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath   = "images.xml";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML and collect all Base64 strings inside <ImageData> elements
        XDocument xDoc = XDocument.Load(xmlPath);
        var base64Images = xDoc
            .Descendants("ImageData")
            .Select(e => e.Value.Trim())
            .Where(v => !string.IsNullOrEmpty(v));

        // Create a new PDF document – wrapped in a using block for deterministic disposal
        using (Document pdfDoc = new Document())
        {
            int pageIndex = 1;
            foreach (string b64 in base64Images)
            {
                // Decode the Base64 string to a byte array
                byte[] imageBytes;
                try
                {
                    imageBytes = Convert.FromBase64String(b64);
                }
                catch (FormatException)
                {
                    Console.Error.WriteLine($"Invalid Base64 data on image #{pageIndex}, skipping.");
                    continue;
                }

                // Add a new page for each image
                Page page = pdfDoc.Pages.Add();

                // Create an Aspose.Pdf.Image and feed it the decoded bytes via a MemoryStream
                // Fully qualify the type to avoid ambiguity with System.Drawing.Image
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = new MemoryStream(imageBytes)
                };

                // Add the image to the page's paragraph collection
                page.Paragraphs.Add(pdfImage);

                pageIndex++;
            }

            // Save the resulting PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with embedded images saved to '{pdfPath}'.");
    }
}