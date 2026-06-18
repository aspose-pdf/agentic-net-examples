using System;
using System.Data;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF file (self‑contained example)
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add();
            pdfDoc.Save("input.pdf");
        }

        // Step 2: Re‑open the PDF and add a table with filtered data
        using (Document pdfDoc = new Document("input.pdf"))
        {
            // Simulate an exported Excel worksheet by building a DataTable
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Score", typeof(int));
            dataTable.Columns.Add("Remarks", typeof(string));

            dataTable.Rows.Add("Alice", 85, "Excellent");
            dataTable.Rows.Add("Bob", 45, "Needs Improvement");
            dataTable.Rows.Add("Charlie", 70, "Good");
            dataTable.Rows.Add("Diana", 30, "Poor");

            // Filter rows where Score >= 50
            List<int> selectedRowIndices = new List<int>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                int score = Convert.ToInt32(dataTable.Rows[i]["Score"]);
                if (score >= 50)
                {
                    selectedRowIndices.Add(i);
                }
            }

            // Prepare column list (all three columns)
            int[] sourceColumnList = new int[] { 0, 1, 2 };
            int[] sourceRowList = selectedRowIndices.ToArray();

            // Add a table to the first page
            Page page = pdfDoc.Pages[1];
            Table table = new Table();
            page.Paragraphs.Add(table);

            // Define column widths (max 4 columns allowed in evaluation mode)
            table.ColumnWidths = "100 100 100"; // string with space‑separated widths

            // Import the filtered data, include column names as the first row
            table.ImportDataTable(dataTable, sourceRowList, sourceColumnList, 0, 0, true, false);

            // Save the resulting PDF
            pdfDoc.Save("output.pdf");
        }
    }
}
