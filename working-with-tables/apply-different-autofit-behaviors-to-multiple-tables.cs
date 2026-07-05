using System;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "tables.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the tables
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // First table – AutoFit to content (columns expand based on cell content)
            // -------------------------------------------------
            Table table1 = new Table
            {
                // Visual styling (optional)
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                // Position on the page
                Left = 50,
                Top = 700
            };

            // Initialise column widths *before* importing data – required for AutoFitToContent
            table1.ColumnWidths = "0 0";

            // Sample data for the first table
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Header1");
            dt1.Columns.Add("Header2");
            dt1.Rows.Add("Short", "A very long piece of text that should cause the column to expand");

            // Import data (first row will be column names)
            table1.ImportDataTable(dt1, true, 0, 0);

            // Apply AutoFit behavior *after* the data has been imported
            table1.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;

            // Add the first table to the page
            page.Paragraphs.Add(table1);

            // -------------------------------------------------
            // Second table – AutoFit to window (columns shrink/expand to fit page width)
            // -------------------------------------------------
            Table table2 = new Table
            {
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                Left = 50,
                Top = 500
            };

            // Initialise column widths *before* importing data – required for AutoFitToWindow
            table2.ColumnWidths = "0 0";

            // Sample data for the second table
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("HeaderA");
            dt2.Columns.Add("HeaderB");
            dt2.Rows.Add("Data1", "Data2");
            dt2.Rows.Add("LongerData1", "LongerData2");

            table2.ImportDataTable(dt2, true, 0, 0);

            // Apply AutoFit behavior *after* the data has been imported
            table2.ColumnAdjustment = ColumnAdjustment.AutoFitToWindow;

            // Add the second table to the page
            page.Paragraphs.Add(table2);

            // Save the PDF with both tables having distinct AutoFit behaviors
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
