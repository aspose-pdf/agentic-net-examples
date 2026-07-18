using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class SerializeTableToXml
{
    static void Main()
    {
        // Resolve absolute paths for the output PDF and XML files to avoid URI format issues
        string pdfPath = Path.GetFullPath("table.pdf");
        string xmlPath = Path.GetFullPath("table.xml");

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set basic properties
            Table table = new Table
            {
                ColumnWidths = "100 150 200", // three columns with specified widths
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Quantity");

            // Add some data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item {i}");
                row.Cells.Add((i * 10).ToString());
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF (optional, just to have a visual file)
            doc.Save(pdfPath);

            // Serialize the entire document (including the table) to XML
            // Using absolute file path prevents UriFormatException inside Aspose.Pdf internals.
            doc.SaveXml(xmlPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}'.");
        Console.WriteLine($"Document XML (including table structure) saved to '{xmlPath}'.");
    }
}
