using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // Path to source PDF
        const string outputFolder = "ExtractedTables";         // Folder for CSV files

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(pdfDoc);

            // Iterate over each discovered table
            for (int tableIndex = 0; tableIndex < absorber.TableList.Count; tableIndex++)
            {
                var absorbedTable = absorber.TableList[tableIndex];
                StringBuilder csvBuilder = new StringBuilder();

                // Process rows
                foreach (var row in absorbedTable.RowList)
                {
                    var cellValues = new List<string>();

                    // Process cells in the current row
                    foreach (var cell in row.CellList)
                    {
                        StringBuilder cellTextBuilder = new StringBuilder();

                        // Concatenate all text fragments inside the cell
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellTextBuilder.Append(fragment.Text);
                        }

                        // Escape CSV special characters
                        string cellText = cellTextBuilder.ToString()
                                                          .Replace("\"", "\"\""); // escape quotes
                        cellValues.Add($"\"{cellText}\"");
                    }

                    // Join cell values with commas and add a new line
                    csvBuilder.AppendLine(string.Join(",", cellValues));
                }

                // Write the CSV content to a file named Table_1.csv, Table_2.csv, etc.
                string csvPath = Path.Combine(outputFolder, $"Table_{tableIndex + 1}.csv");
                File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);
                Console.WriteLine($"Extracted table {tableIndex + 1} to '{csvPath}'.");
            }
        }
    }
}