using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class DiscountCalculator
{
    static void Main()
    {
        // Paths for the XML containing discount rates and the output PDF.
        const string xmlPath = "discounts.xml";
        const string pdfPath = "InvoiceWithDiscounts.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load discount rates from the XML file.
        // Expected format:
        // <Discounts>
        //   <Item name="ItemA" rate="0.10" />
        //   <Item name="ItemB" rate="0.20" />
        // </Discounts>
        var discountRates = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
        try
        {
            XDocument doc = XDocument.Load(xmlPath);
            if (doc.Root == null)
            {
                Console.Error.WriteLine("XML does not contain a root element.");
                return;
            }
            foreach (var elem in doc.Root.Elements("Item"))
            {
                string name = (string)elem.Attribute("name");
                string rateStr = (string)elem.Attribute("rate");
                if (!string.IsNullOrEmpty(name) && double.TryParse(rateStr, out double rate))
                {
                    discountRates[name] = rate;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse XML: {ex.Message}");
            return;
        }

        // Sample items with base prices.
        var items = new List<(string Name, double BasePrice)>
        {
            ("ItemA", 100.0),
            ("ItemB", 250.0),
            ("ItemC", 75.0) // No discount entry – will use 0%
        };

        // Prepare data for the table and calculate totals.
        var tableData = new List<(string Name, double BasePrice, double DiscountRate, double FinalPrice)>();
        double grandTotal = 0.0;

        foreach (var item in items)
        {
            double discount = discountRates.TryGetValue(item.Name, out double d) ? d : 0.0;
            double finalPrice = item.BasePrice * (1.0 - discount);
            grandTotal += finalPrice;
            tableData.Add((item.Name, item.BasePrice, discount, finalPrice));
        }

        // Create a new PDF document.
        using (Document pdfDoc = new Document())
        {
            // Add a page.
            Page page = pdfDoc.Pages.Add();

            // Create a table with 4 columns.
            Table table = new Table
            {
                ColumnWidths = "120 120 120 120", // widths in points
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Header row.
            Row header = table.Rows.Add();
            header.Cells.Add("Item");
            header.Cells.Add("Base Price");
            header.Cells.Add("Discount Rate");
            header.Cells.Add("Final Price");
            foreach (Cell cell in header.Cells)
            {
                cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
                cell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    Font = FontRepository.FindFont("Helvetica")
                };
            }

            // Data rows.
            foreach (var rowData in tableData)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(rowData.Name);
                row.Cells.Add(rowData.BasePrice.ToString("C"));
                row.Cells.Add($"{rowData.DiscountRate:P0}");
                row.Cells.Add(rowData.FinalPrice.ToString("C"));
                foreach (Cell cell in row.Cells)
                {
                    cell.DefaultCellTextState = new TextState
                    {
                        FontSize = 11,
                        Font = FontRepository.FindFont("Helvetica")
                    };
                }
            }

            // Add the table to the page.
            page.Paragraphs.Add(table);

            // Add a paragraph showing the grand total.
            TextFragment totalFragment = new TextFragment($"Grand Total: {grandTotal:C}");
            // Modify the existing TextState instead of assigning a new one.
            totalFragment.TextState.FontSize = 14;
            totalFragment.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            totalFragment.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGreen;
            totalFragment.Position = new Position(50, page.PageInfo.Height - 100); // place below the table
            page.Paragraphs.Add(totalFragment);

            // Save the PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully at '{pdfPath}'.");
    }
}
