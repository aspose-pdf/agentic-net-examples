using System;
using System.IO;
using System.Data;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF containing tables
        const string outputFolder = "ExtractedTables";    // folder where CSV files will be written

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document (lifecycle rule: use provided Visit method)
            absorber.Visit(pdfDoc);

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables were found in the document.");
                return;
            }

            int tableIndex = 0;
            foreach (var absorbedTable in absorber.TableList)
            {
                // Determine the maximum number of cells in any row – this defines column count
                int maxColumns = absorbedTable.RowList
                    .Max(r => r.CellList.Count);

                // Create a DataTable to hold the extracted data
                DataTable dt = new DataTable($"Table{tableIndex + 1}");
                for (int col = 0; col < maxColumns; col++)
                {
                    dt.Columns.Add($"Column{col + 1}");
                }

                // Populate the DataTable row by row
                foreach (var row in absorbedTable.RowList)
                {
                    DataRow dataRow = dt.NewRow();

                    for (int cellIdx = 0; cellIdx < row.CellList.Count; cellIdx++)
                    {
                        var cell = row.CellList[cellIdx];

                        // Concatenate all text fragments inside the cell
                        string cellText = string.Join(" ",
                            cell.TextFragments.Select(tf => tf.Text));

                        dataRow[cellIdx] = cellText;
                    }

                    dt.Rows.Add(dataRow);
                }

                // Write the DataTable to a CSV file
                string csvPath = Path.Combine(outputFolder, $"Table_{tableIndex + 1}.csv");
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // Write header
                    writer.WriteLine(string.Join(",", dt.Columns
                        .Cast<DataColumn>()
                        .Select(c => $"\"{c.ColumnName}\"")));

                    // Write each data row
                    foreach (DataRow dr in dt.Rows)
                    {
                        var fields = dr.ItemArray
                            .Select(field => $"\"{field?.ToString().Replace("\"", "\"\"")}\"");
                        writer.WriteLine(string.Join(",", fields));
                    }
                }

                Console.WriteLine($"Extracted Table {tableIndex + 1} -> {csvPath}");
                tableIndex++;
            }
        }
    }
}