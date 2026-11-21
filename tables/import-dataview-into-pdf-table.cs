using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Ensure an input PDF exists – create a minimal one‑page PDF if it does not.
        if (!File.Exists(inputPdfPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdfPath);
        }

        // Sample DataView (replace with your actual DataView)
        DataTable table = new DataTable();
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Score", typeof(double));
        table.Rows.Add(1, "Alice", 85.5);
        table.Rows.Add(2, "Bob", 92.0);
        table.Rows.Add(3, "Charlie", 78.3);
        DataView dataView = new DataView(table);

        // Load PDF, add table, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a new table
            Table pdfTable = new Table();

            // Set column widths (comma‑separated string). One extra column for the leading empty column.
            int columnCount = dataView.Table.Columns.Count + 1; // +1 for the empty first column
            string[] widths = new string[columnCount];
            widths[0] = "50"; // empty column width
            for (int i = 1; i < columnCount; i++)
                widths[i] = "100";
            pdfTable.ColumnWidths = string.Join(",", widths);

            // Add header row (row 1)
            Row headerRow = pdfTable.Rows.Add();
            // First cell (empty)
            headerRow.Cells.Add(new Cell());
            // Header cells for each column
            foreach (DataColumn col in dataView.Table.Columns)
            {
                Cell headerCell = new Cell();
                TextFragment tf = new TextFragment(col.ColumnName);
                tf.TextState.FontStyle = FontStyles.Bold;
                headerCell.Paragraphs.Add(tf);
                headerRow.Cells.Add(headerCell);
            }

            // Add data rows starting at row 2, column 2
            foreach (DataRowView drv in dataView)
            {
                if (drv == null) continue; // safety check to silence CS8602
                Row dataRow = pdfTable.Rows.Add();
                // First cell (empty)
                dataRow.Cells.Add(new Cell());
                // Data cells
                foreach (DataColumn col in dataView.Table.Columns)
                {
                    object value = drv![col.ColumnName]; // null‑forgiving operator to silence warning
                    Cell dataCell = new Cell();
                    dataCell.Paragraphs.Add(new TextFragment(value?.ToString() ?? string.Empty));
                    dataRow.Cells.Add(dataCell);
                }
            }

            // Add the table to the first page (page index is 1‑based)
            Page page = doc.Pages[1];
            page.Paragraphs.Add(pdfTable);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}
