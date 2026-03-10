using System;
using System.Data;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class ExcelToPdfTable
{
    static void Main()
    {
        // Paths to the target PDF file (Excel input removed for this example)
        const string pdfPath = "output.pdf";

        // -----------------------------------------------------------------
        // 1. Create a sample DataTable that mimics data that would have been
        //    read from an Excel worksheet. This removes the dependency on
        //    Aspose.Cells while keeping the rest of the example unchanged.
        // -----------------------------------------------------------------
        DataTable dataTable = GetSampleDataTable();

        // -----------------------------------------------------------------
        // 2. Create a PDF document and add a table that imports the DataTable.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            // Add a new page to the document.
            Page page = pdfDoc.Pages.Add();

            // Create a Table object. The Table inherits from BaseParagraph,
            // so it can be added directly to the page's Paragraphs collection.
            Table table = new Table();

            // Optional: set column widths to distribute the page width equally.
            if (dataTable.Columns.Count > 0)
            {
                double columnWidth = page.PageInfo.Width / dataTable.Columns.Count;
                string widths = string.Join(" ", Enumerable.Repeat(columnWidth.ToString(), dataTable.Columns.Count));
                table.ColumnWidths = widths;
            }

            // Import the DataTable into the Aspose.Pdf.Table.
            // Parameters:
            //   - dataTable: source data
            //   - true: import column names as the first row of the table
            //   - 0: start importing at the first row of the target table
            //   - 0: start importing at the first column of the target table
            table.ImportDataTable(dataTable, true, 0, 0);

            // Optional: set a border for better visual separation.
            table.Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black);

            // Add the populated table to the page.
            page.Paragraphs.Add(table);

            // -----------------------------------------------------------------
            // 3. Save the PDF document.
            // -----------------------------------------------------------------
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Data has been imported into PDF: {pdfPath}");
    }

    /// <summary>
    /// Generates a sample DataTable with a few columns and rows. In a real
    /// scenario this could be replaced with data loaded from CSV, a database,
    /// or any other source that does not require Aspose.Cells.
    /// </summary>
    private static DataTable GetSampleDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Product", typeof(string));
        dt.Columns.Add("Quantity", typeof(int));
        dt.Columns.Add("Price", typeof(decimal));

        dt.Rows.Add("Apple", 10, 0.5m);
        dt.Rows.Add("Banana", 5, 0.3m);
        dt.Rows.Add("Orange", 8, 0.6m);
        dt.Rows.Add("Grapes", 12, 1.2m);

        return dt;
    }
}
