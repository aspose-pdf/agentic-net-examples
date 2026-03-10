using System;
using System.Data;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class TableExtractor
{
    /// <summary>
    /// Extracts the table at the specified zero‑based index from a PDF file.
    /// Returns the table data as a System.Data.DataTable.
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF document.</param>
    /// <param name="tableIndex">Zero‑based index of the table to extract (0 = first table).</param>
    /// <returns>DataTable containing the extracted table rows and cells.</returns>
    public static DataTable ExtractTable(string pdfPath, int tableIndex)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Create a TableAbsorber to locate tables in the document.
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document.
            absorber.Visit(doc);

            // Validate the requested index.
            if (tableIndex < 0 || tableIndex >= absorber.TableList.Count)
                throw new ArgumentOutOfRangeException(nameof(tableIndex),
                    $"Table index {tableIndex} is out of range. Document contains {absorber.TableList.Count} table(s).");

            // Get the absorbed table.
            AbsorbedTable absorbedTable = absorber.TableList[tableIndex];

            // Determine the maximum number of columns across all rows.
            int maxColumns = 0;
            foreach (var row in absorbedTable.RowList)
            {
                if (row.CellList.Count > maxColumns)
                    maxColumns = row.CellList.Count;
            }

            // Prepare a DataTable with appropriate columns.
            DataTable result = new DataTable();
            for (int col = 0; col < maxColumns; col++)
            {
                result.Columns.Add($"Column{col + 1}", typeof(string));
            }

            // Populate the DataTable row by row.
            foreach (var row in absorbedTable.RowList)
            {
                DataRow dataRow = result.NewRow();

                for (int col = 0; col < row.CellList.Count; col++)
                {
                    var cell = row.CellList[col];
                    StringBuilder cellText = new StringBuilder();

                    // Concatenate all text fragments inside the cell.
                    foreach (TextFragment fragment in cell.TextFragments)
                    {
                        cellText.Append(fragment.Text);
                    }

                    dataRow[col] = cellText.ToString();
                }

                // Cells that are missing (shorter rows) remain null.
                result.Rows.Add(dataRow);
            }

            return result;
        }
    }

    // Example usage.
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const int targetTableIndex = 0; // first table

        try
        {
            DataTable tableData = ExtractTable(inputPdf, targetTableIndex);

            Console.WriteLine($"Extracted {tableData.Rows.Count} rows and {tableData.Columns.Count} columns:");
            foreach (DataRow row in tableData.Rows)
            {
                for (int i = 0; i < tableData.Columns.Count; i++)
                {
                    Console.Write($"{row[i]}\t");
                }
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}