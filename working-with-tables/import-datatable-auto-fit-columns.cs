using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for MarginInfo and BorderInfo if needed

class Program
{
    static void Main()
    {
        const string inputPdf  = "template.pdf";
        const string outputPdf = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -------------------------------------------------
        // Create a sample DataTable to be imported into PDF
        // -------------------------------------------------
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("ID",   typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Price",typeof(double));

        dataTable.Rows.Add(1, "Apple",  0.50);
        dataTable.Rows.Add(2, "Banana", 0.30);
        dataTable.Rows.Add(3, "Cherry", 1.20);

        // -------------------------------------------------
        // Load the PDF, add a table, import data, auto‑fit
        // -------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // Create a new table instance
            Table table = new Table
            {
                // Optional visual settings
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Import the DataTable; include column names as the first row
            table.ImportDataTable(dataTable, true, 0, 0);

            // Adjust column widths automatically based on the imported content
            // Aspose.Pdf.Table does not have AutoFitColumns(); use ColumnAdjustment instead.
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;

            // Position the table on the first page
            Page page = doc.Pages[1];
            table.Left = 50;   // distance from the left edge
            table.Top  = 700;  // distance from the bottom edge

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with auto‑fitted table saved to '{outputPdf}'.");
    }
}
