using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedTables";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            int tableIndex = 0;
            foreach (var absorbedTable in absorber.TableList)
            {
                tableIndex++;
                string csvFile = Path.Combine(outputDir, $"Table_{tableIndex}.csv");

                // Write the extracted table to a CSV file
                using (StreamWriter writer = new StreamWriter(csvFile))
                {
                    foreach (var row in absorbedTable.RowList)
                    {
                        // Build a CSV line by concatenating cell texts
                        var cellValues = row.CellList.Select(cell =>
                        {
                            // Combine all text fragments inside the cell
                            string text = string.Concat(cell.TextFragments.Select(tf => tf.Text));

                            // Escape commas and double quotes according to CSV rules
                            if (text.Contains(',') || text.Contains('\"'))
                            {
                                text = $"\"{text.Replace("\"", "\"\"")}\"";
                            }
                            return text;
                        });

                        writer.WriteLine(string.Join(",", cellValues));
                    }
                }

                Console.WriteLine($"Table {tableIndex} extracted to: {csvFile}");
            }
        }
    }
}