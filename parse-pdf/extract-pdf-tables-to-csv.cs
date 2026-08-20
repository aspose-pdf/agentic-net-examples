using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "TablesCsv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Find all tables in the document
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc); // extracts tables from all pages

            // Iterate over each detected table
            for (int tableIndex = 0; tableIndex < absorber.TableList.Count; tableIndex++)
            {
                var absorbedTable = absorber.TableList[tableIndex];
                StringBuilder csvBuilder = new StringBuilder();

                // Process rows
                foreach (var row in absorbedTable.RowList)
                {
                    var cellTexts = new List<string>();

                    // Process cells in the current row
                    foreach (var cell in row.CellList)
                    {
                        StringBuilder cellBuilder = new StringBuilder();

                        // Concatenate all text fragments inside the cell
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellBuilder.Append(fragment.Text);
                        }

                        // Escape CSV special characters
                        string cellText = cellBuilder.ToString()
                            .Replace("\"", "\"\""); // escape double quotes

                        if (cellText.Contains(",") || cellText.Contains("\"") || cellText.Contains("\n"))
                        {
                            cellText = $"\"{cellText}\"";
                        }

                        cellTexts.Add(cellText);
                    }

                    // Join cells with commas to form a CSV line
                    csvBuilder.AppendLine(string.Join(",", cellTexts));
                }

                // Write the CSV file for the current table
                string csvPath = Path.Combine(outputFolder, $"table_{tableIndex + 1}.csv");
                File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);
                Console.WriteLine($"Exported table {tableIndex + 1} to '{csvPath}'.");
            }
        }
    }
}