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

        // Load PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create TableAbsorber to find tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // If no tables found, inform user
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables were detected in the document.");
                return;
            }

            // Process each detected table
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var absorbedTable = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1} on page {absorbedTable.PageNum}");

                // Create a DataTable to hold the extracted data
                DataTable dt = new DataTable();

                // Determine column count from first row
                int columnCount = absorbedTable.RowList[0].CellList.Count;
                for (int c = 0; c < columnCount; c++)
                {
                    dt.Columns.Add($"Column{c + 1}", typeof(string));
                }

                // Iterate rows
                foreach (var row in absorbedTable.RowList)
                {
                    DataRow dr = dt.NewRow();
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        // Concatenate all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (var fragment in row.CellList[c].TextFragments)
                        {
                            cellText += fragment.Text;
                        }
                        dr[c] = cellText;
                    }
                    dt.Rows.Add(dr);
                }

                // Output extracted data (example: tab‑separated values to console)
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine(string.Join("\t", dr.ItemArray));
                }

                // Optional: save the extracted table to a CSV file
                // SaveDataTableToCsv(dt, $"Table_{t + 1}.csv");
            }
        }
    }

    // Helper method to write a DataTable to CSV (optional)
    static void SaveDataTableToCsv(DataTable table, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write header
            for (int i = 0; i < table.Columns.Count; i++)
            {
                writer.Write(table.Columns[i].ColumnName);
                if (i < table.Columns.Count - 1) writer.Write(",");
            }
            writer.WriteLine();

            // Write rows
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    writer.Write(row[i]?.ToString()?.Replace(",", " "));
                    if (i < table.Columns.Count - 1) writer.Write(",");
                }
                writer.WriteLine();
            }
        }
    }
}