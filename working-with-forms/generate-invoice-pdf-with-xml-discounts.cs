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
        // Paths to the XML file containing discount rates and the output PDF.
        const string xmlPath = "discounts.xml";
        const string pdfPath = "InvoiceWithDiscount.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load discount rates from the XML file.
        //    The XML is expected to have a structure like:
        //    <Discounts>
        //        <Item id="A001" rate="0.10" />
        //        <Item id="B002" rate="0.15" />
        //    </Discounts>
        // -----------------------------------------------------------------
        var discountRates = new Dictionary<string, double>();
        try
        {
            XDocument doc = XDocument.Load(xmlPath);
            foreach (var elem in doc.Root.Elements("Item"))
            {
                string id = (string)elem.Attribute("id");
                double rate = (double?)elem.Attribute("rate") ?? 0.0;
                if (!string.IsNullOrEmpty(id))
                {
                    discountRates[id] = rate;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse XML: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Define a simple list of items with their IDs and base prices.
        // -----------------------------------------------------------------
        var items = new List<(string Id, double Price)>
        {
            ("A001", 120.00),
            ("B002",  80.50),
            ("C003",  45.75) // No discount entry – will use 0% discount.
        };

        // -----------------------------------------------------------------
        // 3. Calculate the total price after applying discounts.
        // -----------------------------------------------------------------
        double total = 0.0;
        foreach (var item in items)
        {
            double discount = discountRates.TryGetValue(item.Id, out double rate) ? rate : 0.0;
            double discountedPrice = item.Price * (1.0 - discount);
            total += discountedPrice;
        }

        // -----------------------------------------------------------------
        // 4. Create a PDF document and write the calculated total.
        //    Use the recommended lifecycle pattern (using block) for disposal.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            // Add a blank page.
            Page page = pdfDoc.Pages.Add();

            // Prepare the text to display.
            string resultText = $"Total after discounts: {total:C2}";

            // Create a TextFragment with the result.
            TextFragment fragment = new TextFragment(resultText)
            {
                // Position the text near the top-left corner.
                Position = new Position(50, 750),

                // Optional styling.
                TextState = { FontSize = 14, Font = FontRepository.FindFont("Helvetica") }
            };

            // Add the fragment to the page.
            page.Paragraphs.Add(fragment);

            // Save the PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Invoice PDF generated: {pdfPath}");
    }
}