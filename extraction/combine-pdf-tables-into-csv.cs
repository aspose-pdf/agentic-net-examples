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
        const string outputCsvPath = "AllTables.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // TableAbsorber extracts tables in the default (document order) sequence
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(pdfDoc); // <-- correct way to run the absorber

            // Prepare a StreamWriter for the CSV output
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false, Encoding.UTF8))
            {
                // Iterate over each extracted table
                foreach (var absorbedTable in absorber.TableList)
                {
                    // Iterate over rows
                    foreach (var row in absorbedTable.RowList)
                    {
                        // Build a CSV line from the cells
                        StringBuilder lineBuilder = new StringBuilder();
                        for (int i = 0; i < row.CellList.Count; i++)
                        {
                            var cell = row.CellList[i];
                            // Concatenate all text fragments inside the cell
                            StringBuilder cellTextBuilder = new StringBuilder();
                            if (cell.TextFragments != null)
                            {
                                foreach (var fragment in cell.TextFragments)
                                {
                                    cellTextBuilder.Append(fragment.Text);
                                }
                            }
                            string cellText = cellTextBuilder.ToString();
                            // Escape double quotes by doubling them and wrap the field in quotes
                            cellText = cellText?.Replace("\"", "\"\"") ?? string.Empty;
                            lineBuilder.Append('"').Append(cellText).Append('"');

                            if (i < row.CellList.Count - 1)
                                lineBuilder.Append(','); // column separator
                        }

                        writer.WriteLine(lineBuilder.ToString());
                    }

                    // Insert an empty line to separate tables (optional)
                    writer.WriteLine();
                }
            }
        }

        Console.WriteLine($"All tables have been combined into '{outputCsvPath}'.");
    }
}
