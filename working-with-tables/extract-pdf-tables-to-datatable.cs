using System;
using System.IO;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber and related types are in this namespace

class TableExtractionExample
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document (Visit(Document) overload)
            absorber.Visit(doc);

            // Iterate over each detected table
            int tableIndex = 0;
            foreach (var absorbedTable in absorber.TableList)
            {
                // Determine the maximum number of columns in this table
                int maxColumns = 0;
                foreach (var row in absorbedTable.RowList)
                {
                    if (row.CellList.Count > maxColumns)
                        maxColumns = row.CellList.Count;
                }

                // Create a DataTable to hold the extracted data
                DataTable dataTable = new DataTable($"Table_{tableIndex}");

                // Add columns to the DataTable
                for (int col = 0; col < maxColumns; col++)
                {
                    dataTable.Columns.Add($"Column{col + 1}", typeof(string));
                }

                // Populate rows
                foreach (var row in absorbedTable.RowList)
                {
                    DataRow dataRow = dataTable.NewRow();

                    int colIndex = 0;
                    foreach (var cell in row.CellList)
                    {
                        // Concatenate all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        dataRow[colIndex] = cellText;
                        colIndex++;
                    }

                    // Remaining columns (if any) stay null
                    dataTable.Rows.Add(dataRow);
                }

                // Example usage: output basic info about the extracted table
                Console.WriteLine($"Extracted Table {tableIndex}: {dataTable.Rows.Count} rows, {dataTable.Columns.Count} columns.");

                // TODO: further processing of 'dataTable' as needed (e.g., export to CSV, database, etc.)

                tableIndex++;
            }
        }
    }
}