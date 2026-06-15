using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace PdfReportMultipleTables
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a blank PDF file to work with (self‑contained example)
            using (Document createDoc = new Document())
            {
                createDoc.Pages.Add();
                createDoc.Save("input.pdf");
            }

            // Step 2: Open the created PDF and add tables
            using (Document doc = new Document("input.pdf"))
            {
                // First DataTable
                DataTable dataTable1 = new DataTable("Employees");
                DataColumn columnId = new DataColumn("ID", typeof(int));
                DataColumn columnName = new DataColumn("Name", typeof(string));
                dataTable1.Columns.Add(columnId);
                dataTable1.Columns.Add(columnName);

                DataRow row1 = dataTable1.NewRow();
                row1["ID"] = 1;
                row1["Name"] = "Alice";
                dataTable1.Rows.Add(row1);

                DataRow row2 = dataTable1.NewRow();
                row2["ID"] = 2;
                row2["Name"] = "Bob";
                dataTable1.Rows.Add(row2);

                // Create first table and import data
                Table table1 = new Table();
                table1.ColumnWidths = "50 150";
                table1.ImportDataTable(dataTable1, true, 0, 0);

                // Add first table to the first page
                doc.Pages[1].Paragraphs.Add(table1);

                // Second DataTable
                DataTable dataTable2 = new DataTable("Products");
                DataColumn columnProduct = new DataColumn("Product", typeof(string));
                DataColumn columnPrice = new DataColumn("Price", typeof(decimal));
                dataTable2.Columns.Add(columnProduct);
                dataTable2.Columns.Add(columnPrice);

                DataRow prodRow1 = dataTable2.NewRow();
                prodRow1["Product"] = "Pen";
                prodRow1["Price"] = 1.20m;
                dataTable2.Rows.Add(prodRow1);

                DataRow prodRow2 = dataTable2.NewRow();
                prodRow2["Product"] = "Notebook";
                prodRow2["Price"] = 3.45m;
                dataTable2.Rows.Add(prodRow2);

                // Create second table and import data
                Table table2 = new Table();
                table2.ColumnWidths = "150 50";
                table2.ImportDataTable(dataTable2, true, 0, 0);

                // Add a new page for the second table
                doc.Pages.Add();
                doc.Pages[2].Paragraphs.Add(table2);

                // Save the final report
                doc.Save("output.pdf");
            }
        }
    }
}
