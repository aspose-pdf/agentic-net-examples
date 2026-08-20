using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare a sample DataTable with three rows and two columns
        DataTable sourceTable = new DataTable("Sample");
        sourceTable.Columns.Add("ID", typeof(int));
        sourceTable.Columns.Add("Name", typeof(string));

        sourceTable.Rows.Add(1, "Alice");
        sourceTable.Rows.Add(2, "Bob");
        sourceTable.Rows.Add(3, "Charlie");

        // Create a new PDF document
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document())
        {
            // For each DataRow create a separate page with a table containing that row
            foreach (DataRow dr in sourceTable.Rows)
            {
                // Add a new page
                Aspose.Pdf.Page page = pdfDoc.Pages.Add();

                // Create a table with the same number of columns as the DataTable
                Aspose.Pdf.Table table = new Aspose.Pdf.Table();

                // Optional: set column widths (equal distribution). ColumnWidths expects a string.
                int columnCount = sourceTable.Columns.Count;
                string widths = string.Join(" ", new string[columnCount].Select(_ => "200")); // e.g., "200 200"
                table.ColumnWidths = widths;

                // Add a single row to the table
                Aspose.Pdf.Row pdfRow = table.Rows.Add();

                // Fill cells with the values from the current DataRow
                foreach (DataColumn col in sourceTable.Columns)
                {
                    string cellText = dr[col].ToString();
                    // Each cell must contain a Paragraph (TextFragment) – not a raw string.
                    Cell cell = new Cell();
                    cell.Paragraphs.Add(new TextFragment(cellText));
                    pdfRow.Cells.Add(cell);
                }

                // Add the table to the page
                page.Paragraphs.Add(table);
            }

            // Save the PDF
            string outputPath = "output.pdf";
            pdfDoc.Save(outputPath);

            // Verification: the number of pages should equal the number of rows in the source DataTable
            if (pdfDoc.Pages.Count == sourceTable.Rows.Count)
            {
                Console.WriteLine($"Verification succeeded: PDF contains {pdfDoc.Pages.Count} pages, matching the {sourceTable.Rows.Count} rows in the DataTable.");
            }
            else
            {
                Console.WriteLine($"Verification failed: PDF contains {pdfDoc.Pages.Count} pages, but the DataTable has {sourceTable.Rows.Count} rows.");
            }
        }
    }
}
