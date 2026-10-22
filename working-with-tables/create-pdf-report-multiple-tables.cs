using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfReportGenerator
{
    static void Main()
    {
        // Create sample DataTables (replace with actual DB queries)
        DataTable dtCustomers = GetCustomersDataTable();
        DataTable dtOrders = GetOrdersDataTable();

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add first table on first page
            AddDataTableToPdf(pdfDoc, dtCustomers, "Customers");

            // Add second table on second page
            AddDataTableToPdf(pdfDoc, dtOrders, "Orders");

            // Save the PDF report
            pdfDoc.Save("Report.pdf");
        }

        Console.WriteLine("PDF report generated: Report.pdf");
    }

    // Adds a DataTable as a PDF table on a new page with a title
    static void AddDataTableToPdf(Document doc, DataTable dataTable, string title)
    {
        // Add a new page
        Page page = doc.Pages.Add();

        // Add title as a paragraph
        TextFragment titleFragment = new TextFragment(title);
        titleFragment.TextState.FontSize = 16;
        titleFragment.TextState.FontStyle = FontStyles.Bold;
        titleFragment.TextState.Font = FontRepository.FindFont("Arial");
        page.Paragraphs.Add(titleFragment);

        // Create a PDF table
        Table pdfTable = new Table();

        // Set table border and cell padding
        pdfTable.Border = new BorderInfo(BorderSide.All, 0.5f);
        pdfTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);
        pdfTable.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

        // Add header row
        Row headerRow = pdfTable.Rows.Add();
        foreach (DataColumn column in dataTable.Columns)
        {
            Cell headerCell = headerRow.Cells.Add(column.ColumnName);
            headerCell.BackgroundColor = Aspose.Pdf.Color.LightGray;
            // Use DefaultCellTextState to format header text (TextInfo is not available in this version)
            headerCell.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                Font = FontRepository.FindFont("Arial"),
                ForegroundColor = Aspose.Pdf.Color.Black
            };
        }

        // Add data rows
        foreach (DataRow dr in dataTable.Rows)
        {
            Row dataRow = pdfTable.Rows.Add();
            foreach (object item in dr.ItemArray)
            {
                dataRow.Cells.Add(item?.ToString() ?? string.Empty);
            }
        }

        // Adjust column widths (simple fixed width; customize as needed)
        int colCount = dataTable.Columns.Count;
        string[] widths = new string[colCount];
        for (int i = 0; i < colCount; i++)
        {
            widths[i] = "100";
        }
        pdfTable.ColumnWidths = string.Join(" ", widths);

        // Add the table to the page
        page.Paragraphs.Add(pdfTable);
    }

    // Sample DataTable for customers
    static DataTable GetCustomersDataTable()
    {
        DataTable dt = new DataTable("Customers");
        dt.Columns.Add("CustomerID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Country", typeof(string));

        dt.Rows.Add(1, "Alice", "USA");
        dt.Rows.Add(2, "Bob", "UK");
        dt.Rows.Add(3, "Charlie", "Canada");
        return dt;
    }

    // Sample DataTable for orders
    static DataTable GetOrdersDataTable()
    {
        DataTable dt = new DataTable("Orders");
        dt.Columns.Add("OrderID", typeof(int));
        dt.Columns.Add("CustomerID", typeof(int));
        dt.Columns.Add("Amount", typeof(decimal));

        dt.Rows.Add(1001, 1, 250.00m);
        dt.Rows.Add(1002, 2, 150.50m);
        dt.Rows.Add(1003, 1, 300.75m);
        return dt;
    }
}
