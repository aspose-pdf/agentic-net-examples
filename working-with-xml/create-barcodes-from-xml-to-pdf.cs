using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace (type prefixes will be fully qualified)
using Aspose.Pdf.Forms;               // For BarcodeField
using Aspose.Pdf.Annotations;         // For Annotation collections

class Program
{
    static void Main()
    {
        // Paths to the source XML (used as a template) and the resulting PDF.
        const string xmlPath   = "input.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML into a PDF document using XmlLoadOptions.
        // The XML is expected to contain the visual layout of the PDF.
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Parse the same XML to extract barcode field definitions.
            // Expected XML format (example):
            // <Barcodes>
            //   <BarcodeField name="Code1" value="1234567890"
            //                 page="1" x="100" y="500" width="200" height="50" />
            //   ...
            // </Barcodes>
            XDocument xDoc = XDocument.Load(xmlPath);
            foreach (XElement bf in xDoc.Descendants("BarcodeField"))
            {
                // Retrieve attributes; provide defaults where appropriate.
                string fieldName = (string)bf.Attribute("name") ?? "BarcodeField";
                string value     = (string)bf.Attribute("value") ?? string.Empty;
                int    pageIndex = (int?)bf.Attribute("page") ?? 1; // 1‑based page index
                double x          = (double?)bf.Attribute("x") ?? 0;
                double y          = (double?)bf.Attribute("y") ?? 0;
                double w          = (double?)bf.Attribute("width") ?? 100;
                double h          = (double?)bf.Attribute("height") ?? 50;

                // Ensure the requested page exists.
                if (pageIndex < 1 || pageIndex > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageIndex} does not exist. Skipping barcode '{fieldName}'.");
                    continue;
                }

                // Define the rectangle where the barcode will be placed.
                // Aspose.Pdf.Rectangle constructor expects (llx, lly, urx, ury).
                Rectangle rect = new Rectangle(x, y, x + w, y + h);

                // Create the barcode field on the target page.
                BarcodeField barcodeField = new BarcodeField(pdfDoc.Pages[pageIndex], rect);

                // Set a unique name for the field (used for form processing).
                barcodeField.Name = fieldName;

                // Generate the barcode (Code128) with the supplied value.
                barcodeField.AddBarcode(value);

                // Optional visual styling.
                barcodeField.Color = Color.Black;

                // Add the barcode field to the page's annotation collection.
                pdfDoc.Pages[pageIndex].Annotations.Add(barcodeField);
            }

            // Save the final PDF with the generated barcodes.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with barcodes saved to '{outputPdf}'.");
    }
}
