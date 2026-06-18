using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "TablesCsv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Create a TableAbsorber to find tables in the whole document
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from all pages
                absorber.Visit(doc);

                // Iterate over each detected table
                for (int i = 0; i < absorber.TableList.Count; i++)
                {
                    var table = absorber.TableList[i];
                    string csvPath = Path.Combine(outputDir, $"Table_{i + 1}.csv");

                    // Write the table data to a CSV file
                    using (StreamWriter writer = new StreamWriter(csvPath))
                    {
                        // Process each row in the table
                        for (int r = 0; r < table.RowList.Count; r++)
                        {
                            var row = table.RowList[r];
                            string[] cells = new string[row.CellList.Count];

                            // Process each cell in the row
                            for (int c = 0; c < row.CellList.Count; c++)
                            {
                                var cell = row.CellList[c];
                                // Concatenate all text fragments within the cell
                                string cellText = "";
                                foreach (var fragment in cell.TextFragments)
                                {
                                    cellText += fragment.Text;
                                }

                                // Escape CSV special characters
                                cellText = cellText.Replace("\"", "\"\"");
                                if (cellText.Contains(",") || cellText.Contains("\"") || cellText.Contains("\n"))
                                {
                                    cellText = $"\"{cellText}\"";
                                }

                                cells[c] = cellText;
                            }

                            // Write the CSV line for the current row
                            writer.WriteLine(string.Join(",", cells));
                        }
                    }

                    Console.WriteLine($"Exported table {i + 1} to {csvPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}