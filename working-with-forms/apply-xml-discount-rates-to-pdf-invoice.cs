using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath      = "discounts.xml";
        const string pdfTemplate  = "invoice_template.pdf";
        const string pdfOutput    = "invoice_with_total.pdf";

        // -----------------------------------------------------------------
        // 1. Load discount rates from the XML file.
        // -----------------------------------------------------------------
        // Expected XML format:
        // <Discounts>
        //   <Discount code="A" rate="0.10" />
        //   <Discount code="B" rate="0.15" />
        // </Discounts>
        var discountRates = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            XDocument xDoc = XDocument.Load(xmlPath);
            foreach (var elem in xDoc.Descendants("Discount"))
            {
                string code = (string)elem.Attribute("code");
                string rateStr = (string)elem.Attribute("rate");
                if (!string.IsNullOrWhiteSpace(code) && double.TryParse(rateStr, out double rate))
                {
                    discountRates[code] = rate;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse XML: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Load the PDF template.
        // -----------------------------------------------------------------
        if (!File.Exists(pdfTemplate))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplate}");
            return;
        }

        using (Document pdfDoc = new Document(pdfTemplate))
        {
            // -----------------------------------------------------------------
            // 3. Perform a dynamic total price calculation.
            // -----------------------------------------------------------------
            double basePrice = 1234.56;               // base amount before discount
            string discountCode = "A";                // code to look up in the XML

            double discountRate = 0.0;
            if (discountRates.TryGetValue(discountCode, out double rate))
                discountRate = rate;                  // e.g., 0.10 for 10%

            double discountedAmount = basePrice * (1 - discountRate);
            string totalText = $"Discount Code: {discountCode}\n" +
                               $"Base Price: {basePrice:C2}\n" +
                               $"Discount: {discountRate:P0}\n" +
                               $"Total: {discountedAmount:C2}";

            // -----------------------------------------------------------------
            // 4. Add the calculated total to the PDF as a text fragment.
            // -----------------------------------------------------------------
            Page page = pdfDoc.Pages[1];
            // Position the fragment – X = 50, Y = 750 (points from bottom‑left).
            TextFragment fragment = new TextFragment(totalText)
            {
                Position = new Position(50, 750),
                TextState =
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };

            page.Paragraphs.Add(fragment);

            // -----------------------------------------------------------------
            // 5. Save the modified PDF.
            // -----------------------------------------------------------------
            pdfDoc.Save(pdfOutput);
        }

        Console.WriteLine($"PDF saved to '{pdfOutput}'.");
    }
}
