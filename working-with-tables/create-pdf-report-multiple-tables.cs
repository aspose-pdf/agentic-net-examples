using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "Report.pdf";

        // Simulate first database table
        DataTable dt1 = new DataTable("Employees");
        dt1.Columns.Add("ID", typeof(int));
        dt1.Columns.Add("Name", typeof(string));
        dt1.Columns.Add("Department", typeof(string));
        dt1.Rows.Add(1, "Alice", "HR");
        dt1.Rows.Add(2, "Bob", "IT");
        dt1.Rows.Add(3, "Charlie", "Finance");

        // Simulate second database table
        DataTable dt2 = new DataTable("Products");
        dt2.Columns.Add("SKU", typeof(string));
        dt2.Columns.Add("ProductName", typeof(string));
        dt2.Columns.Add("Price", typeof(decimal));
        dt2.Rows.Add("A001", "Widget", 19.99m);
        dt2.Rows.Add("A002", "Gadget", 29.99m);
        dt2.Rows.Add("A003", "Doohickey", 9.99m);

        // Create PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Title for first table
            TextFragment title1 = new TextFragment("Employee List");
            title1.TextState.FontSize = 14;
            title1.TextState.Font = FontRepository.FindFont("Helvetica");
            title1.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
            title1.Position = new Position(0, 800);
            page.Paragraphs.Add(title1);

            // First table
            Table table1 = new Table();
            table1.ColumnWidths = "100 150 150";
            // Use BorderInfo constructor instead of non‑existent Border class
            table1.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);
            table1.ImportDataTable(dt1, true, 0, 0);
            table1.Margin = new MarginInfo { Top = 20 };
            page.Paragraphs.Add(table1);

            // Spacer
            page.Paragraphs.Add(new TextFragment("\n"));

            // Title for second table
            TextFragment title2 = new TextFragment("Product Catalog");
            title2.TextState.FontSize = 14;
            title2.TextState.Font = FontRepository.FindFont("Helvetica");
            title2.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGreen;
            title2.Position = new Position(0, 500);
            page.Paragraphs.Add(title2);

            // Second table
            Table table2 = new Table();
            table2.ColumnWidths = "80 200 80";
            table2.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);
            table2.ImportDataTable(dt2, true, 0, 0);
            table2.Margin = new MarginInfo { Top = 20 };
            page.Paragraphs.Add(table2);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF report generated: {outputPath}");
    }
}
