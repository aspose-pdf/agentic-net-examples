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
        const string outputFolder = "ExtractedTables";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // TableAbsorber extracts tables from the document
                TableAbsorber absorber = new TableAbsorber();

                // Visit the whole document (or each page) to collect tables
                absorber.Visit(doc);

                if (absorber.TableList.Count == 0)
                {
                    Console.WriteLine("No tables were found in the document.");
                    return;
                }

                // Iterate over each detected table
                for (int t = 0; t < absorber.TableList.Count; t++)
                {
                    var table = absorber.TableList[t]; // AbsorbedTable
                    string csvPath = Path.Combine(outputFolder, $"Table_{t + 1}.csv");

                    // Write CSV using UTF‑8 encoding
                    using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
                    {
                        // Iterate rows
                        foreach (var row in table.RowList) // AbsorbedRow
                        {
                            StringBuilder line = new StringBuilder();

                            // Iterate cells within the row
                            for (int c = 0; c < row.CellList.Count; c++)
                            {
                                var cell = row.CellList[c]; // AbsorbedCell

                                // Concatenate all text fragments inside the cell
                                string cellText = string.Empty;
                                if (cell.TextFragments != null && cell.TextFragments.Count > 0)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    foreach (TextFragment frag in cell.TextFragments)
                                    {
                                        sb.Append(frag.Text);
                                    }
                                    cellText = sb.ToString();
                                }

                                // Escape double quotes by doubling them
                                cellText = cellText?.Replace("\"", "\"\"") ?? string.Empty;
                                // Enclose each field in double quotes (CSV standard)
                                line.Append('"').Append(cellText).Append('"');

                                if (c < row.CellList.Count - 1)
                                    line.Append(','); // column separator
                            }

                            writer.WriteLine(line.ToString());
                        }
                    }

                    Console.WriteLine($"Table {t + 1} saved to '{csvPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
