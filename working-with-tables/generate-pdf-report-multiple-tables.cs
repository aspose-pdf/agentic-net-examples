using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class ReportGenerator
{
    static void Main()
    {
        // Prepare data from different sources (mocked here as in‑memory DataTables)
        DataTable firstTableData  = GetFirstSourceData();
        DataTable secondTableData = GetSecondSourceData();

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // ---------- First Table ----------
            // Add a new page for the first table
            Page page1 = pdfDoc.Pages.Add();

            // Create the table and configure basic appearance
            Table table1 = new Table();
            table1.ColumnWidths = "100 150 100"; // three columns
            table1.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black);
            table1.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // Import the DataTable (include column names as the first row)
            table1.ImportDataTable(firstTableData, true, 0, 0);

            // Add the table to the page
            page1.Paragraphs.Add(table1);

            // ---------- Second Table ----------
            // Add another page for the second table
            Page page2 = pdfDoc.Pages.Add();

            // Create the second table
            Table table2 = new Table();
            table2.ColumnWidths = "200 200"; // two columns
            table2.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.DarkGray);
            table2.DefaultCellPadding = new MarginInfo(4, 4, 4, 4);

            // Import the second DataTable
            table2.ImportDataTable(secondTableData, true, 0, 0);

            // Add the second table to its page
            page2.Paragraphs.Add(table2);

            // Save the assembled PDF report
            pdfDoc.Save("Report.pdf");
        }
    }

    // Mock method simulating data retrieval from the first database
    static DataTable GetFirstSourceData()
    {
        DataTable dt = new DataTable("FirstSource");
        dt.Columns.Add("ID",    typeof(int));
        dt.Columns.Add("Name",  typeof(string));
        dt.Columns.Add("Score", typeof(double));

        dt.Rows.Add(1, "Alice",   85.5);
        dt.Rows.Add(2, "Bob",     92.0);
        dt.Rows.Add(3, "Charlie", 78.3);

        return dt;
    }

    // Mock method simulating data retrieval from the second database
    static DataTable GetSecondSourceData()
    {
        DataTable dt = new DataTable("SecondSource");
        dt.Columns.Add("Product",   typeof(string));
        dt.Columns.Add("Quantity",  typeof(int));

        dt.Rows.Add("Widget",   150);
        dt.Rows.Add("Gadget",    80);
        dt.Rows.Add("Doohickey", 45);

        return dt;
    }
}