using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "TablesCsv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF document (using rule for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Find all tables in the document
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc); // extracts tables from the whole document

            // Iterate over each discovered table
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                StringBuilder sb = new StringBuilder();

                // Iterate rows
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];
                    var cellTexts = new string[row.CellList.Count];

                    // Iterate cells
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        StringBuilder cellBuilder = new StringBuilder();

                        // Concatenate all text fragments inside the cell
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            cellBuilder.Append(fragment.Text);
                        }

                        // Escape CSV special characters
                        string cellText = cellBuilder.ToString();
                        if (cellText.Contains("\""))
                            cellText = cellText.Replace("\"", "\"\""); // escape quotes
                        if (cellText.Contains(",") || cellText.Contains("\n") || cellText.Contains("\r") || cellText.Contains("\""))
                            cellText = $"\"{cellText}\"";

                        cellTexts[c] = cellText;
                    }

                    // Join cells with commas and add line
                    sb.AppendLine(string.Join(",", cellTexts));
                }

                // Write CSV file for this table
                string csvPath = Path.Combine(outputFolder, $"table_{t + 1}.csv");
                File.WriteAllText(csvPath, sb.ToString(), Encoding.UTF8);
                Console.WriteLine($"Table {t + 1} exported to: {csvPath}");
            }
        }
    }
}