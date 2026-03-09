using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted_tables.txt";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Create TableAbsorber to find tables in the document
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the whole document
                absorber.Visit(doc);

                // Write extracted table data (including headings) to a text file
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    for (int t = 0; t < absorber.TableList.Count; t++)
                    {
                        var table = absorber.TableList[t];
                        writer.WriteLine($"Table {t + 1}:");

                        // Assume the first row contains heading information
                        if (table.RowList.Count > 0)
                        {
                            var headerRow = table.RowList[0];
                            writer.Write("Header: ");
                            for (int c = 0; c < headerRow.CellList.Count; c++)
                            {
                                var cell = headerRow.CellList[c];
                                // Concatenate all text fragments within the cell
                                string cellText = "";
                                foreach (var fragment in cell.TextFragments)
                                {
                                    cellText += fragment.Text;
                                }
                                writer.Write(cellText);
                                if (c < headerRow.CellList.Count - 1) writer.Write(" | ");
                            }
                            writer.WriteLine();
                        }

                        // Optionally write all rows of the table
                        for (int r = 0; r < table.RowList.Count; r++)
                        {
                            var row = table.RowList[r];
                            writer.Write($"Row {r + 1}: ");
                            for (int c = 0; c < row.CellList.Count; c++)
                            {
                                var cell = row.CellList[c];
                                string cellText = "";
                                foreach (var fragment in cell.TextFragments)
                                {
                                    cellText += fragment.Text;
                                }
                                writer.Write(cellText);
                                if (c < row.CellList.Count - 1) writer.Write(" | ");
                            }
                            writer.WriteLine();
                        }

                        writer.WriteLine(); // Separate tables
                    }
                }

                Console.WriteLine($"Table data extracted to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}