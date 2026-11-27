using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "CsvTables";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int tableIndex = 1;

            // Iterate through each page because TableAbsorber works per page
            foreach (Page page in pdfDoc.Pages)
            {
                // Absorb tables on the current page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                foreach (AbsorbedTable absorbedTable in absorber.TableList)
                {
                    // Ensure the table has at least one row
                    if (absorbedTable.RowList == null || absorbedTable.RowList.Count == 0)
                        continue;

                    // Extract text from the first row cells
                    var firstRow = absorbedTable.RowList[0];
                    var cellTexts = firstRow.CellList.Select(cell =>
                    {
                        // A cell may contain multiple TextFragments; concatenate them
                        if (cell.TextFragments == null || cell.TextFragments.Count == 0)
                            return string.Empty;
                        return string.Join(" ", cell.TextFragments.Select(tf => tf.Text));
                    });

                    // Build CSV line (simple comma‑separated, values are not escaped for brevity)
                    string csvLine = string.Join(",", cellTexts);

                    // Write the CSV file for this table (single‑row CSV)
                    string csvPath = Path.Combine(outputFolder, $"Table_{tableIndex}.csv");
                    File.WriteAllText(csvPath, csvLine + Environment.NewLine);

                    Console.WriteLine($"Saved first row of table {tableIndex} to '{csvPath}'");
                    tableIndex++;
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}
