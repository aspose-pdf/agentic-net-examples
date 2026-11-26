using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF (use absolute paths to avoid URI issues)
        string sourcePdfPath = Path.GetFullPath("template.pdf");   // existing PDF (can be blank)
        string outputPdfPath = Path.GetFullPath("custom_pageinfo.pdf");

        // Ensure the source PDF exists; create a simple one if not
        if (!File.Exists(sourcePdfPath))
        {
            using (Document doc = new Document())
            {
                // Add a single empty page
                doc.Pages.Add();
                doc.Save(sourcePdfPath);
            }
        }

        // Build the custom PageInfo XML
        XDocument pageInfoXml = new XDocument(
            new XElement("PageInfo",
                new XElement("Height", "842"),          // A4 height in points
                new XElement("Width", "595"),           // A4 width in points
                new XElement("IsLandscape", "false"),
                new XElement("Margin", "36")            // 0.5 inch margin (36 points)
            )
        );

        // Convert XML to a stream for binding (BindXml expects a file path or a stream)
        byte[] xmlBytes = Encoding.UTF8.GetBytes(pageInfoXml.ToString());
        using (var xmlStream = new MemoryStream(xmlBytes))
        {
            // Load the PDF, bind the XML, and save the result
            using (Document pdfDoc = new Document(sourcePdfPath))
            {
                // Bind the custom XML data to the PDF. Using a stream avoids the UriFormatException.
                pdfDoc.BindXml(xmlStream);

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF with custom PageInfo saved to '{outputPdfPath}'.");
    }
}
