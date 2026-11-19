using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare first DataTable
        DataTable dt1 = new DataTable("Employees");
        dt1.Columns.Add("ID", typeof(int));
        dt1.Columns.Add("Name", typeof(string));
        dt1.Columns.Add("Department", typeof(string));
        dt1.Rows.Add(1, "Alice", "HR");
        dt1.Rows.Add(2, "Bob", "Finance");
        dt1.Rows.Add(3, "Charlie", "IT");

        // Prepare second DataTable
        DataTable dt2 = new DataTable("Products");
        dt2.Columns.Add("SKU", typeof(string));
        dt2.Columns.Add("Product", typeof(string));
        dt2.Columns.Add("Price", typeof(decimal));
        dt2.Rows.Add("A001", "Laptop", 1200.00m);
        dt2.Rows.Add("B023", "Mouse", 25.50m);
        dt2.Rows.Add("C045", "Keyboard", 45.75m);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // ---------- First Table (AutoFit = true) ----------
            Page page1 = doc.Pages.Add();

            Table table1 = new Table
            {
                // Stretch columns to fit the page width
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,
                // Optional default column width (will be overridden by AutoFit)
                DefaultColumnWidth = "100", // Fixed: use string to match property type
                // Default cell padding and text style
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                DefaultCellTextState = new TextState { FontSize = 10 }
            };

            // Header row
            Row headerRow1 = table1.Rows.Add();
            headerRow1.Cells.Add("ID");
            headerRow1.Cells.Add("Name");
            headerRow1.Cells.Add("Department");
            foreach (Cell cell in headerRow1.Cells)
            {
                cell.BackgroundColor = Color.LightGray;
                cell.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };
            }

            // Data rows
            foreach (DataRow dr in dt1.Rows)
            {
                Row dataRow = table1.Rows.Add();
                dataRow.Cells.Add(dr["ID"].ToString());
                dataRow.Cells.Add(dr["Name"].ToString());
                dataRow.Cells.Add(dr["Department"].ToString());
            }

            page1.Paragraphs.Add(table1);

            // ---------- Second Table (AutoFit = false) ----------
            Page page2 = doc.Pages.Add();

            Table table2 = new Table
            {
                // Keep explicit column widths
                ColumnAdjustment = ColumnAdjustment.Customized,
                ColumnWidths = "80 200 80",
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                DefaultCellTextState = new TextState { FontSize = 10 }
            };

            // Header row
            Row headerRow2 = table2.Rows.Add();
            headerRow2.Cells.Add("SKU");
            headerRow2.Cells.Add("Product");
            headerRow2.Cells.Add("Price");
            foreach (Cell cell in headerRow2.Cells)
            {
                cell.BackgroundColor = Color.LightGray;
                cell.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };
            }

            // Data rows
            foreach (DataRow dr in dt2.Rows)
            {
                Row dataRow = table2.Rows.Add();
                dataRow.Cells.Add(dr["SKU"].ToString());
                dataRow.Cells.Add(dr["Product"].ToString());
                dataRow.Cells.Add(dr["Price"].ToString());
            }

            page2.Paragraphs.Add(table2);

            // Save the PDF document
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with multiple tables created successfully.");
    }
}
