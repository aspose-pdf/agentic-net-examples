using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string xmlPath   = "data.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file and convert it to a PDF document.
        // XmlLoadOptions tells Aspose.Pdf to treat the source as XML.
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, xmlLoadOptions))
        {
            // Parse the same XML to obtain barcode field definitions.
            // Expected XML format:
            // <BarcodeField name="Field1" value="123456" x="100" y="500" width="200" height="50" />
            XDocument xdoc = XDocument.Load(xmlPath);
            foreach (var bf in xdoc.Descendants("BarcodeField"))
            {
                string fieldName = (string)bf.Attribute("name") ?? "Barcode";
                string value     = (string)bf.Attribute("value") ?? "";
                double x          = (double?)bf.Attribute("x") ?? 100;
                double y          = (double?)bf.Attribute("y") ?? 500;
                double width      = (double?)bf.Attribute("width") ?? 200;
                double height     = (double?)bf.Attribute("height") ?? 50;

                // Ensure the document has at least one page.
                if (pdfDoc.Pages.Count == 0)
                {
                    pdfDoc.Pages.Add();
                }

                // Use the first page for barcode placement (adjust as needed).
                Page page = pdfDoc.Pages[1];

                // Define the rectangle where the barcode will appear.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + width, y + height);

                // Create a barcode field on the page.
                BarcodeField barcode = new BarcodeField(page, rect)
                {
                    Name  = fieldName,
                    Color = Aspose.Pdf.Color.Black
                };

                // Generate a Code128 barcode with the supplied value.
                barcode.AddBarcode(value);

                // Add the barcode annotation to the page.
                page.Annotations.Add(barcode);
            }

            // Save the final PDF containing all generated barcodes.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with barcodes saved to '{outputPdf}'.");
    }
}