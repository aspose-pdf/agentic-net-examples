using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";

        // Ensure the file exists before proceeding
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Iterate over each discovered table
            int tableIndex = 0;
            foreach (AbsorbedTable absorbedTable in absorber.TableList)
            {
                // Create a DataTable to hold the extracted data
                DataTable dataTable = new DataTable($"Table{tableIndex}");

                // Determine the maximum number of cells in any row (column count)
                int maxColumns = 0;
                foreach (AbsorbedRow row in absorbedTable.RowList)
                {
                    if (row.CellList.Count > maxColumns)
                        maxColumns = row.CellList.Count;
                }

                // Add columns to the DataTable
                for (int col = 0; col < maxColumns; col++)
                {
                    dataTable.Columns.Add($"Column{col + 1}", typeof(string));
                }

                // Populate rows
                foreach (AbsorbedRow row in absorbedTable.RowList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    int colIndex = 0;

                    foreach (AbsorbedCell cell in row.CellList)
                    {
                        // Concatenate all text fragments within the cell
                        string cellText = string.Empty;
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        // Assign the concatenated text to the appropriate column
                        dataRow[colIndex] = cellText;
                        colIndex++;
                    }

                    // If the row has fewer cells than maxColumns, remaining columns stay null
                    dataTable.Rows.Add(dataRow);
                }

                // Example output: display the extracted table contents
                Console.WriteLine($"--- {dataTable.TableName} (Rows: {dataTable.Rows.Count}, Columns: {dataTable.Columns.Count}) ---");
                foreach (DataRow dr in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        Console.Write($"{dr[i]}\t");
                    }
                    Console.WriteLine();
                }

                tableIndex++;
            }
        }
    }
}