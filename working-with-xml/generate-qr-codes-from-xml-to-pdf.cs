using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";        // XML source containing data to encode
        const string templatePath = "template.pdf"; // Existing PDF to place QR codes on
        const string outputPath = "output.pdf";    // Resulting PDF with QR codes

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // Load XML document
        XDocument xmlDoc = XDocument.Load(xmlPath);
        // Expecting elements like <Item><Value>...</Value></Item>
        var items = xmlDoc.Root?.Elements("Item");
        if (items == null)
        {
            Console.Error.WriteLine("No <Item> elements found in XML.");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(templatePath))
        {
            int pageNumber = 1; // 1‑based indexing as required by Aspose.Pdf

            foreach (var item in items)
            {
                // Ensure there is a page to place the QR code
                if (pageNumber > pdfDoc.Pages.Count)
                {
                    pdfDoc.Pages.Add(); // add a new blank page if needed
                }

                Page page = pdfDoc.Pages[pageNumber];

                // Extract the text to encode into the QR code
                string qrText = (string)item.Element("Value") ?? string.Empty;

                // Define the rectangle where the QR code will be drawn (adjust as needed)
                var rect = new Aspose.Pdf.Rectangle(100, 600, 300, 700);

                // Create a textbox field, add it to the form and draw the barcode (QR) into it
                TextBoxField qrField = new TextBoxField(page, rect)
                {
                    PartialName = $"qr_{pageNumber}"
                };
                pdfDoc.Form.Add(qrField, pageNumber);
                qrField.AddBarcode(qrText); // draws the QR code into the field

                pageNumber++;
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"QR codes have been placed and saved to '{outputPath}'.");
    }
}
