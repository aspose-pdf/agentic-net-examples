using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // First table – AutoFit to content (simulated by column widths)
            // -------------------------------------------------
            DataTable dt1 = new DataTable("Employees");
            dt1.Columns.Add("ID", typeof(int));
            dt1.Columns.Add("Name", typeof(string));
            dt1.Columns.Add("Salary", typeof(decimal));
            dt1.Rows.Add(1, "John Doe", 50000);
            dt1.Rows.Add(2, "Jane Smith", 62000);
            dt1.Rows.Add(3, "Bob Johnson", 47000);

            // Title for the first table
            TextFragment tf1 = new TextFragment("Employees Table (AutoFit to Content)");
            tf1.Position = new Position(0, 800);
            tf1.TextState.Font = FontRepository.FindFont("Arial");
            tf1.TextState.FontSize = 14;
            page.Paragraphs.Add(tf1);

            // Create the first table and populate it manually
            Table table1 = CreateTableFromDataTable(dt1, "50 150 100");
            page.Paragraphs.Add(table1);

            // -------------------------------------------------
            // Second table – AutoFit to page width (simulated by column widths)
            // -------------------------------------------------
            DataTable dt2 = new DataTable("Products");
            dt2.Columns.Add("Product", typeof(string));
            dt2.Columns.Add("Quantity", typeof(int));
            dt2.Columns.Add("Price", typeof(double));
            dt2.Rows.Add("Apple", 120, 0.5);
            dt2.Rows.Add("Banana", 80, 0.3);
            dt2.Rows.Add("Orange", 150, 0.6);

            // Add a blank line for spacing
            page.Paragraphs.Add(new TextFragment("\n"));

            // Title for the second table
            TextFragment tf2 = new TextFragment("Products Table (AutoFit to Page Width)");
            tf2.Position = new Position(0, 600);
            tf2.TextState.Font = FontRepository.FindFont("Arial");
            tf2.TextState.FontSize = 14;
            page.Paragraphs.Add(tf2);

            // Create the second table
            Table table2 = CreateTableFromDataTable(dt2, "150 80 80");
            page.Paragraphs.Add(table2);

            // -------------------------------------------------
            // Third table – AutoFit to page height (simulated by column widths)
            // -------------------------------------------------
            DataTable dt3 = new DataTable("Scores");
            dt3.Columns.Add("Student", typeof(string));
            dt3.Columns.Add("Math", typeof(int));
            dt3.Columns.Add("Science", typeof(int));
            dt3.Columns.Add("English", typeof(int));
            dt3.Rows.Add("Alice", 85, 90, 88);
            dt3.Rows.Add("Mark", 78, 82, 80);
            dt3.Rows.Add("Eve", 92, 95, 94);

            // Add a blank line for spacing
            page.Paragraphs.Add(new TextFragment("\n"));

            // Title for the third table
            TextFragment tf3 = new TextFragment("Scores Table (AutoFit to Page Height)");
            tf3.Position = new Position(0, 400);
            tf3.TextState.Font = FontRepository.FindFont("Arial");
            tf3.TextState.FontSize = 14;
            page.Paragraphs.Add(tf3);

            // Create the third table
            Table table3 = CreateTableFromDataTable(dt3, "120 60 60 60");
            page.Paragraphs.Add(table3);

            // Save the PDF document
            string outputPath = "TablesDemo.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to {outputPath}");
        }
    }

    /// <summary>
    /// Creates an Aspose.Pdf.Table from a DataTable.
    /// The column widths are supplied as a space‑separated string (e.g., "50 150 100").
    /// The method adds a header row followed by data rows and applies a default cell border.
    /// </summary>
    private static Table CreateTableFromDataTable(DataTable dt, string columnWidths)
    {
        Table table = new Table
        {
            ColumnWidths = columnWidths,
            DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
        };

        // Header row
        Row header = table.Rows.Add();
        foreach (DataColumn col in dt.Columns)
        {
            Cell cell = header.Cells.Add(col.ColumnName);
            cell.DefaultCellTextState = new TextState { Font = FontRepository.FindFont("Arial"), FontSize = 12, FontStyle = FontStyles.Bold };
        }

        // Data rows
        foreach (DataRow dr in dt.Rows)
        {
            Row row = table.Rows.Add();
            foreach (object item in dr.ItemArray)
            {
                row.Cells.Add(item?.ToString() ?? string.Empty);
            }
        }

        return table;
    }
}
