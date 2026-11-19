using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// Suppress known package vulnerability warnings (NU1902, NU1903)
#pragma warning disable NU1902 // Package 'System.Security.Cryptography.Pkcs' has a known moderate severity vulnerability
#pragma warning disable NU1903 // Package 'System.Security.Cryptography.Pkcs' has a known high severity vulnerability

class Program
{
    static void Main()
    {
        const string outputPdf = "table.pdf";
        const string outputXml = "table.xml";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page (first page is at index 1)
            Page page = doc.Pages.Add();

            // Create a table with 3 columns
            Table table = new Table
            {
                ColumnWidths = "100 150 200", // column widths in points
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Description");
            // Style header cells
            foreach (Cell cell in header.Cells)
            {
                cell.BackgroundColor = Color.LightGray;
                cell.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };
            }

            // Add a few data rows
            for (int i = 1; i <= 3; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item {i}");
                row.Cells.Add($"This is a description for item {i}.");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF (optional, just to have a visual file)
            doc.Save(outputPdf);

            // -----------------------------------------------------------------
            // Serialize the table structure to a custom XML representation
            // -----------------------------------------------------------------
            XDocument xmlDoc = new XDocument(
                new XElement("Table",
                    // Iterate over each row in the Aspose.Pdf.Table
                    IterateRows(table)
                )
            );

            // Save the generated XML to disk
            xmlDoc.Save(outputXml);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
        Console.WriteLine($"XML representation saved to '{outputXml}'.");
    }

    /// <summary>
    /// Converts the rows of an Aspose.Pdf.Table into a collection of XElement objects.
    /// </summary>
    private static object IterateRows(Table table)
    {
        // Use a LINQ style projection to build the XML for each row
        var rows = new System.Collections.Generic.List<XElement>();
        foreach (Row r in table.Rows)
        {
            var rowElem = new XElement("Row");
            foreach (Cell c in r.Cells)
            {
                // Each cell may contain one or more paragraphs; we extract the first TextFragment if present.
                string cellText = string.Empty;
                if (c.Paragraphs.Count > 0 && c.Paragraphs[0] is TextFragment tf)
                {
                    cellText = tf.Text;
                }
                rowElem.Add(new XElement("Cell", cellText));
            }
            rows.Add(rowElem);
        }
        return rows;
    }
}
#pragma warning restore NU1902
#pragma warning restore NU1903