using System;
using System.IO;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(pdfDoc);

            // Iterate over each detected table
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var absorbedTable = absorber.TableList[t];

                // Prepare a DataTable to hold the extracted tabular data
                DataTable dt = new DataTable($"Table_{t + 1}");

                // Determine the maximum number of columns across all rows
                int maxCols = 0;
                foreach (var row in absorbedTable.RowList)
                {
                    if (row.CellList.Count > maxCols)
                        maxCols = row.CellList.Count;
                }

                // Add columns to the DataTable
                for (int c = 0; c < maxCols; c++)
                {
                    dt.Columns.Add($"Column{c + 1}", typeof(string));
                }

                // Populate the DataTable with cell contents
                foreach (var row in absorbedTable.RowList)
                {
                    DataRow dr = dt.NewRow();
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        // Concatenate all text fragments within the cell
                        string cellText = string.Empty;
                        foreach (var fragment in row.CellList[c].TextFragments)
                        {
                            cellText += fragment.Text;
                        }
                        dr[c] = cellText;
                    }
                    dt.Rows.Add(dr);
                }

                // Example output: write the table as CSV to the console
                Console.WriteLine($"--- Table {t + 1} ---");

                // Header line
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    Console.Write(dt.Columns[c].ColumnName);
                    if (c < dt.Columns.Count - 1) Console.Write(",");
                }
                Console.WriteLine();

                // Data rows
                foreach (DataRow dr in dt.Rows)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        // Replace commas inside cell text to keep CSV format simple
                        Console.Write(dr[c]?.ToString()?.Replace(",", " "));
                        if (c < dt.Columns.Count - 1) Console.Write(",");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}