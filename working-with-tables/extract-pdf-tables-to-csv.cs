using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDirectory = "ExtractedTables";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPdfPath))
            {
                // Create a TableAbsorber to find tables in the document
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the whole document
                absorber.Visit(doc);

                int tableIndex = 1;
                foreach (var absorbedTable in absorber.TableList)
                {
                    StringBuilder csvBuilder = new StringBuilder();

                    // Iterate over rows
                    foreach (var row in absorbedTable.RowList)
                    {
                        List<string> cellValues = new List<string>();

                        // Iterate over cells in the row
                        foreach (var cell in row.CellList)
                        {
                            // Concatenate all text fragments inside the cell
                            StringBuilder cellTextBuilder = new StringBuilder();
                            foreach (var fragment in cell.TextFragments)
                            {
                                cellTextBuilder.Append(fragment.Text);
                            }

                            // Escape CSV special characters
                            string cellText = cellTextBuilder.ToString()
                                .Replace("\"", "\"\""); // escape double quotes

                            if (cellText.Contains(",") || cellText.Contains("\"") || cellText.Contains("\n"))
                            {
                                cellText = $"\"{cellText}\"";
                            }

                            cellValues.Add(cellText);
                        }

                        // Join cell values with commas and add a new line
                        csvBuilder.AppendLine(string.Join(",", cellValues));
                    }

                    // Write the CSV file for the current table
                    string csvPath = Path.Combine(outputDirectory, $"table_{tableIndex}.csv");
                    File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);
                    Console.WriteLine($"Table {tableIndex} extracted to: {csvPath}");
                    tableIndex++;
                }

                if (absorber.TableList.Count == 0)
                {
                    Console.WriteLine("No tables were found in the document.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during table extraction: {ex.Message}");
        }
    }
}