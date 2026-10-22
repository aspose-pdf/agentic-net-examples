using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "template.pdf";   // existing PDF to host the table
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Prepare a sample DataTable
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Product", typeof(string));
        dataTable.Columns.Add("Quantity", typeof(int));
        dataTable.Columns.Add("Price", typeof(decimal));

        dataTable.Rows.Add("Apple",  10, 0.5m);
        dataTable.Rows.Add("Banana", 20, 0.3m);
        dataTable.Rows.Add("Cherry", 15, 1.2m);

        // Define a column‑to‑table mapping (DataTable column name -> Table column index)
        Dictionary<string, int> columnMapping = new Dictionary<string, int>
        {
            { "Product",  0 },   // maps to first column of the table
            { "Quantity", 1 },   // maps to second column
            { "Price",    2 }    // maps to third column
        };

        // Load the PDF and insert the table
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a table with three columns (the number of columns must match the highest index in the mapping + 1)
            Table table = new Table();
            table.ColumnWidths = "150 100 100"; // optional: set column widths

            // ---- Add header row (optional) ----
            Row headerRow = table.Rows.Add();
            // Ensure cells are created for each column index in order
            int columnCount = columnMapping.Count;
            for (int i = 0; i < columnCount; i++)
            {
                Cell headerCell = headerRow.Cells.Add();
                // Find the column name that maps to this index
                string headerText = null;
                foreach (var kvp in columnMapping)
                {
                    if (kvp.Value == i)
                    {
                        headerText = kvp.Key;
                        break;
                    }
                }
                headerCell.Paragraphs.Add(new TextFragment(headerText ?? string.Empty) { TextState = { FontStyle = FontStyles.Bold } });
            }

            // ---- Populate table rows using the mapping ----
            foreach (DataRow dr in dataTable.Rows)
            {
                Row row = table.Rows.Add();
                // Create empty cells first to guarantee correct order
                for (int i = 0; i < columnCount; i++)
                {
                    row.Cells.Add();
                }

                foreach (var map in columnMapping)
                {
                    string dtColumnName = map.Key;
                    int tableColumnIndex = map.Value;
                    object value = dr[dtColumnName];
                    string text = value?.ToString() ?? string.Empty;
                    // The cell at the required index already exists
                    Cell cell = row.Cells[tableColumnIndex];
                    cell.Paragraphs.Add(new TextFragment(text));
                }
            }

            // Add a title above the table (optional)
            TextFragment title = new TextFragment("Sales Report");
            title.TextState.FontSize = 14;
            title.TextState.FontStyle = FontStyles.Bold;
            doc.Pages[1].Paragraphs.Add(title);

            // Add the table to the first page
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with mapped table saved to '{outputPdfPath}'.");
    }
}
