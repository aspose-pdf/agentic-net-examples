using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;               // Required for text-related types (not used here but safe)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath   = "input.xml";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document (no special load options required for plain XML)
        XDocument xDoc = XDocument.Load(xmlPath);

        // Create a new PDF document and add the first page
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add(); // page index will be 1 (Aspose.Pdf uses 1‑based indexing)

            // Find all attribute values that contain a base64 data URI (e.g. src="data:image/png;base64,....")
            var base64Attributes = xDoc.Descendants()
                .SelectMany(e => e.Attributes())
                .Where(a => a.Value.Contains("base64,"))
                .Select(a => a.Value);

            // Also check element values that might directly contain the base64 string
            var base64Elements = xDoc.Descendants()
                .Where(e => e.Value.Contains("base64,"))
                .Select(e => e.Value);

            // Combine both sources
            var allBase64Strings = base64Attributes.Concat(base64Elements).ToList();

            int pageIndex = 1; // current page (starts at 1)

            foreach (string dataUri in allBase64Strings)
            {
                // Extract the part after "base64,"
                int commaPos = dataUri.IndexOf("base64,", StringComparison.Ordinal);
                if (commaPos < 0) continue; // safety check

                string base64Part = dataUri.Substring(commaPos + 7);
                byte[] imageBytes;

                try
                {
                    imageBytes = Convert.FromBase64String(base64Part);
                }
                catch (FormatException)
                {
                    // Skip malformed base64 strings
                    continue;
                }

                // If more than one image, create a new page for each subsequent image
                if (pageIndex > 1)
                {
                    pdfDoc.Pages.Add();
                }

                // Create an Aspose.Pdf.Image and assign the decoded stream
                Image img = new Image
                {
                    ImageStream = new MemoryStream(imageBytes)
                };

                // Add the image to the current page. Adjust the rectangle as needed.
                // Here we place it at (50, 500) with a width/height of 400x400 points.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 500, 450, 900);
                pdfDoc.Pages[pageIndex].Paragraphs.Add(img);
                // Optionally, you could also use Page.AddImage(stream, rect) instead:
                // pdfDoc.Pages[pageIndex].AddImage(new MemoryStream(imageBytes), rect);

                pageIndex++;
            }

            // Save the resulting PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created with embedded images: {pdfPath}");
    }
}