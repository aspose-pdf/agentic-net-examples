using System;
using System.IO;
using System.Data;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string xmlPath = "discounts.xml";
        const string outputPdf = "Invoice.pdf";

        // Load discount rates from XML into a dictionary (category -> rate)
        var discounts = LoadDiscounts(xmlPath);

        // Prepare a DataTable with sample items and calculated discounts
        DataTable itemsTable = new DataTable("Items");
        itemsTable.Columns.Add("Item", typeof(string));
        itemsTable.Columns.Add("Category", typeof(string));
        itemsTable.Columns.Add("Price", typeof(decimal));
        itemsTable.Columns.Add("Discount", typeof(decimal));
        itemsTable.Columns.Add("FinalPrice", typeof(decimal));

        // Add sample items (price will be adjusted according to the discount rates)
        AddItem(itemsTable, "Widget", "A", 100m, discounts);
        AddItem(itemsTable, "Gadget", "B", 200m, discounts);
        AddItem(itemsTable, "Thingamajig", "C", 150m, discounts);

        // Compute totals for the invoice
        decimal totalBefore = 0m;
        decimal totalDiscount = 0m;
        decimal totalAfter = 0m;
        foreach (DataRow row in itemsTable.Rows)
        {
            totalBefore += (decimal)row["Price"];
            totalDiscount += (decimal)row["Discount"];
            totalAfter += (decimal)row["FinalPrice"];
        }

        // Create a PDF document using Aspose.Pdf lifecycle rules
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Add a title to the page
            TextFragment title = new TextFragment("Invoice");
            title.Position = new Position(0, 800);
            title.TextState.FontSize = 20;
            title.TextState.Font = FontRepository.FindFont("Helvetica");
            title.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
            page.Paragraphs.Add(title);

            // Create a table to display items and pricing
            Table table = new Table
            {
                ColumnWidths = "100 80 80 80 80",
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray)
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Item");
            header.Cells.Add("Category");
            header.Cells.Add("Price");
            header.Cells.Add("Discount");
            header.Cells.Add("Final Price");

            // Data rows
            foreach (DataRow dr in itemsTable.Rows)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(dr["Item"].ToString());
                row.Cells.Add(dr["Category"].ToString());
                row.Cells.Add(((decimal)dr["Price"]).ToString("C"));
                row.Cells.Add(((decimal)dr["Discount"]).ToString("C"));
                row.Cells.Add(((decimal)dr["FinalPrice"]).ToString("C"));
            }

            // Totals row – insert an empty placeholder cell to occupy the second column.
            Row totalRow = table.Rows.Add();
            totalRow.Cells.Add("TOTAL");          // first column
            totalRow.Cells.Add("");               // placeholder for second column (acts like a span)
            totalRow.Cells.Add(totalBefore.ToString("C"));
            totalRow.Cells.Add(totalDiscount.ToString("C"));
            totalRow.Cells.Add(totalAfter.ToString("C"));

            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDoc.Save(outputPdf);
            }
            else
            {
                try
                {
                    pdfDoc.Save(outputPdf);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine($"PDF processing completed. Output path: {outputPdf}");
    }

    // Parses the XML file and returns a dictionary of discount rates per category
    static Dictionary<string, decimal> LoadDiscounts(string xmlFile)
    {
        var dict = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"Discount XML not found: {xmlFile}");
            return dict;
        }

        XDocument doc = XDocument.Load(xmlFile);
        foreach (var elem in doc.Descendants("Discount"))
        {
            string category = (string)elem.Attribute("Category") ?? string.Empty;
            string rateStr = (string)elem.Attribute("Rate") ?? "0";
            if (decimal.TryParse(rateStr, out decimal rate))
            {
                dict[category] = rate;
            }
        }
        return dict;
    }

    // Adds an item to the DataTable and calculates its discount based on the category
    static void AddItem(DataTable table, string itemName, string category, decimal price,
        Dictionary<string, decimal> discounts)
    {
        decimal rate = 0m;
        discounts.TryGetValue(category, out rate);
        decimal discountAmount = price * rate;
        decimal finalPrice = price - discountAmount;

        DataRow row = table.NewRow();
        row["Item"] = itemName;
        row["Category"] = category;
        row["Price"] = price;
        row["Discount"] = discountAmount;
        row["FinalPrice"] = finalPrice;
        table.Rows.Add(row);
    }

    // Helper that walks the inner‑exception chain to detect a missing native GDI+ library.
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
