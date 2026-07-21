using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the source PDF file containing tables
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Iterate over each detected table
            int tableIndex = 1;
            foreach (AbsorbedTable absorbedTable in absorber.TableList)
            {
                // Create a DataTable to hold the extracted data
                DataTable dataTable = new DataTable($"Table_{tableIndex}");

                // Determine the maximum number of columns in the table
                int maxColumns = 0;
                foreach (AbsorbedRow row in absorbedTable.RowList)
                {
                    if (row.CellList.Count > maxColumns)
                        maxColumns = row.CellList.Count;
                }

                // Add columns to the DataTable
                for (int col = 0; col < maxColumns; col++)
                {
                    dataTable.Columns.Add($"Column_{col + 1}", typeof(string));
                }

                // Populate rows
                foreach (AbsorbedRow row in absorbedTable.RowList)
                {
                    DataRow dataRow = dataTable.NewRow();

                    for (int col = 0; col < row.CellList.Count; col++)
                    {
                        AbsorbedCell cell = row.CellList[col];

                        // Concatenate all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        dataRow[col] = cellText;
                    }

                    dataTable.Rows.Add(dataRow);
                }

                // Example usage: write the DataTable to console
                Console.WriteLine($"--- Extracted Table {tableIndex} ---");
                foreach (DataColumn column in dataTable.Columns)
                {
                    Console.Write($"{column.ColumnName}\t");
                }
                Console.WriteLine();

                foreach (DataRow dr in dataTable.Rows)
                {
                    foreach (var item in dr.ItemArray)
                    {
                        Console.Write($"{item}\t");
                    }
                    Console.WriteLine();
                }

                tableIndex++;
            }
        }
    }
}