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
        // Output PDF path
        const string outputPath = "Report.pdf";

        // Create dummy DataTables representing data from different databases
        DataTable customersTable = CreateCustomersTable();
        DataTable ordersTable    = CreateOrdersTable();

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // -------------------------------------------------
            // First page – Customers table
            // -------------------------------------------------
            Page page1 = pdfDoc.Pages.Add();

            // Create a Table instance
            Table customersPdfTable = new Table
            {
                // Optional visual styling
                BackgroundColor = Aspose.Pdf.Color.LightGray,
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths – required to avoid NullReferenceException during ImportDataTable
            customersPdfTable.ColumnWidths = string.Join(" ", Enumerable.Repeat("100", customersTable.Columns.Count));

            // Import the DataTable into the PDF Table
            // Parameters: (DataTable, import column names, first row, first column)
            customersPdfTable.ImportDataTable(customersTable, true, 0, 0);

            // Position the table on the page
            customersPdfTable.Left = 50;   // distance from left edge
            customersPdfTable.Top  = 750;  // distance from bottom edge

            // Add the table to the page
            page1.Paragraphs.Add(customersPdfTable);

            // -------------------------------------------------
            // Second page – Orders table
            // -------------------------------------------------
            Page page2 = pdfDoc.Pages.Add();

            Table ordersPdfTable = new Table
            {
                BackgroundColor = Aspose.Pdf.Color.LightYellow,
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.DarkGray),
                DefaultCellPadding = new MarginInfo(4, 4, 4, 4)
            };

            // Define column widths for the orders table as well
            ordersPdfTable.ColumnWidths = string.Join(" ", Enumerable.Repeat("100", ordersTable.Columns.Count));

            ordersPdfTable.ImportDataTable(ordersTable, true, 0, 0);
            ordersPdfTable.Left = 50;
            ordersPdfTable.Top  = 750;

            page2.Paragraphs.Add(ordersPdfTable);

            // -------------------------------------------------
            // Save the PDF document
            // -------------------------------------------------
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF report generated: {Path.GetFullPath(outputPath)}");
    }

    // Helper method to create a dummy Customers DataTable
    private static DataTable CreateCustomersTable()
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

    // Helper method to create a dummy Orders DataTable
    private static DataTable CreateOrdersTable()
    {
        DataTable dt = new DataTable("Orders");
        dt.Columns.Add("OrderID", typeof(int));
        dt.Columns.Add("CustomerID", typeof(int));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("Date", typeof(DateTime));

        dt.Rows.Add(1001, 1, 250.75m, new DateTime(2023, 1, 15));
        dt.Rows.Add(1002, 2, 120.00m, new DateTime(2023, 2, 3));
        dt.Rows.Add(1003, 1, 560.40m, new DateTime(2023, 3, 22));

        return dt;
    }
}
