using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source XML and the resulting PDF
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";

        // Verify that the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML and convert it to a PDF document
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Ensure the document has at least one page
            Page page = pdfDoc.Pages.Count > 0 ? pdfDoc.Pages[1] : pdfDoc.Pages.Add();

            // Define the rectangle where the barcode will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a barcode field on the page
            BarcodeField barcodeField = new BarcodeField(page, barcodeRect)
            {
                // Optional: give the field a name for later reference
                Name = "SampleBarcode"
            };

            // The data to encode in the barcode.
            // In a real scenario this could be read from the XML document.
            string barcodeData = "1234567890";

            // Generate the barcode (Code 128) and make the field read‑only
            barcodeField.AddBarcode(barcodeData);

            // Add the barcode field to the page's annotation collection
            page.Annotations.Add(barcodeField);

            // Save the PDF with the embedded barcode
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with barcode saved to '{outputPdf}'.");
    }
}