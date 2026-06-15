using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF document
        using (Document pdfDocument = new Document())
        {
            // Add a page to the document
            Page page = pdfDocument.Pages.Add();

            // Create a table with three columns
            Table pdfTable = new Table();
            pdfTable.ColumnWidths = "100 100 100";

            // Simulate filling a DataTable from a database query
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Score", typeof(double));

            DataRow row1 = dataTable.NewRow();
            row1["ID"] = 1;
            row1["Name"] = "Alice";
            row1["Score"] = 85.5;
            dataTable.Rows.Add(row1);

            DataRow row2 = dataTable.NewRow();
            row2["ID"] = 2;
            row2["Name"] = "Bob";
            row2["Score"] = 92.0;
            dataTable.Rows.Add(row2);

            DataRow row3 = dataTable.NewRow();
            row3["ID"] = 3;
            row3["Name"] = "Charlie";
            row3["Score"] = 78.0;
            dataTable.Rows.Add(row3);

            // Import the DataTable into the PDF table (include column names as the first row)
            pdfTable.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(pdfTable);

            // Save the resulting PDF
            pdfDocument.Save("output.pdf");
        }
    }
}