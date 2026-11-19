using System;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfReportGenerator
{
    static void Main()
    {
        // Obtain DataTables from databases (mocked here)
        DataTable dt1 = GetFirstDataTable();
        DataTable dt2 = GetSecondDataTable();

        const string outputPath = "Report.pdf";

        // Ensure deterministic disposal of the Document
        using (Document doc = new Document())
        {
            // Add first table on the first page
            AddTableToDocument(doc, dt1, "First Data Table");

            // Add a new page for the second table
            doc.Pages.Add();
            AddTableToDocument(doc, dt2, "Second Data Table");

            // Save the PDF report
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF report saved to '{outputPath}'.");
    }

    // Adds a titled table to the last page of the document
    static void AddTableToDocument(Document doc, DataTable dataTable, string title)
    {
        // Ensure at least one page exists
        if (doc.Pages.Count == 0)
            doc.Pages.Add();

        // Use the last page (1‑based indexing)
        Page page = doc.Pages[doc.Pages.Count];

        // Add a title above the table
        TextFragment titleFragment = new TextFragment(title);
        titleFragment.TextState.FontSize = 14;
        titleFragment.TextState.FontStyle = FontStyles.Bold;
        titleFragment.Position = new Position(0, page.PageInfo.Height - 50);
        page.Paragraphs.Add(titleFragment);

        // Determine column widths (simple equal widths)
        int colCount = dataTable.Columns.Count;
        string widths = string.Join(" ", Enumerable.Repeat("100", colCount));

        // Create the table
        Table table = new Table
        {
            ColumnWidths = widths,
            Border = new BorderInfo(BorderSide.All, 0.5f),
            Margin = new MarginInfo { Top = 60, Bottom = 20, Left = 20, Right = 20 }
        };

        // Header row
        Row header = table.Rows.Add();
        foreach (DataColumn col in dataTable.Columns)
        {
            Cell cell = header.Cells.Add(col.ColumnName);
            cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
            cell.Border = new BorderInfo(BorderSide.All, 0.5f);
        }

        // Data rows
        foreach (DataRow dr in dataTable.Rows)
        {
            Row row = table.Rows.Add();
            foreach (object item in dr.ItemArray)
            {
                Cell cell = row.Cells.Add(item?.ToString() ?? string.Empty);
                cell.Border = new BorderInfo(BorderSide.All, 0.5f);
            }
        }

        // Add the table to the page
        page.Paragraphs.Add(table);
    }

    // Mock method: replace with real database retrieval
    static DataTable GetFirstDataTable()
    {
        DataTable dt = new DataTable("Employees");
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Department", typeof(string));

        dt.Rows.Add(1, "Alice", "HR");
        dt.Rows.Add(2, "Bob", "IT");
        dt.Rows.Add(3, "Charlie", "Finance");
        return dt;
    }

    // Mock method: replace with real database retrieval
    static DataTable GetSecondDataTable()
    {
        DataTable dt = new DataTable("Products");
        dt.Columns.Add("SKU", typeof(string));
        dt.Columns.Add("ProductName", typeof(string));
        dt.Columns.Add("Price", typeof(decimal));

        dt.Rows.Add("A001", "Widget", 19.99m);
        dt.Rows.Add("B002", "Gadget", 29.99m);
        dt.Rows.Add("C003", "Doohickey", 9.99m);
        return dt;
    }
}