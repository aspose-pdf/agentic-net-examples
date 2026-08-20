using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class TableToCsvExtractor
{
    static void Main()
    {
        // Input PDF path
        const string inputPdf = "input.pdf";
        // Output folder for CSV files
        const string outputFolder = "ExtractedTables";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF document (using rule: wrap Document in using)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create TableAbsorber to find tables on the current page
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the page
                absorber.Visit(page);

                // If no tables were found, continue to next page
                if (absorber.TableList == null || absorber.TableList.Count == 0)
                    continue;

                // Process each found table
                for (int tableIndex = 0; tableIndex < absorber.TableList.Count; tableIndex++)
                {
                    // Build CSV file name: Table_page{page}_index{table}.csv
                    string csvFileName = $"table_page{pageIndex}_table{tableIndex + 1}.csv";
                    string csvPath = Path.Combine(outputFolder, csvFileName);

                    using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
                    {
                        // Iterate rows of the absorbed table
                        var absorbedTable = absorber.TableList[tableIndex];
                        foreach (var absorbedRow in absorbedTable.RowList)
                        {
                            // Build a CSV line for the current row
                            StringBuilder lineBuilder = new StringBuilder();

                            // Iterate cells in the row
                            for (int cellIdx = 0; cellIdx < absorbedRow.CellList.Count; cellIdx++)
                            {
                                var cell = absorbedRow.CellList[cellIdx];

                                // Concatenate all text fragments inside the cell
                                StringBuilder cellTextBuilder = new StringBuilder();
                                foreach (var fragment in cell.TextFragments)
                                {
                                    cellTextBuilder.Append(fragment.Text);
                                }

                                // Escape double quotes by doubling them and wrap the field in quotes
                                string cellText = cellTextBuilder.ToString().Replace("\"", "\"\"");
                                lineBuilder.Append('\"').Append(cellText).Append('\"');

                                // Add comma separator if not the last cell
                                if (cellIdx < absorbedRow.CellList.Count - 1)
                                    lineBuilder.Append(',');
                            }

                            // Write the constructed CSV line
                            writer.WriteLine(lineBuilder.ToString());
                        }
                    }

                    Console.WriteLine($"Extracted table saved to: {csvPath}");
                }
            }
        }
    }
}