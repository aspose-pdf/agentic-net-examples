using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing; // for Rectangle (if needed for other drawing tasks)

class Program
{
    static void Main()
    {
        const string xmlPath = "barcodes.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML that defines barcode data
        XDocument xdoc = XDocument.Load(xmlPath);
        if (xdoc.Root == null)
        {
            Console.Error.WriteLine("XML does not contain a root element.");
            return;
        }

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a blank page where barcodes will be placed
            Page page = pdfDoc.Pages.Add();

            // Iterate over each <Barcode> element in the XML
            foreach (var elem in xdoc.Root.Elements("Barcode"))
            {
                string value = (string)elem.Attribute("Value") ?? string.Empty;
                // Symbology handling is omitted because the Aspose.Pdf.Barcode namespace is not available in the core library.
                // The default symbology (Code128) will be used.

                // Define the rectangle for the barcode field (adjust coordinates as needed)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a barcode field on the page
                BarcodeField barcode = new BarcodeField(page, rect);

                // Add the barcode using the default Code128 symbology
                barcode.AddBarcode(value);

                // Add the barcode annotation to the page
                page.Annotations.Add(barcode);
            }

            // Save the PDF containing the generated barcodes
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with barcodes saved to '{outputPdf}'.");
    }
}
