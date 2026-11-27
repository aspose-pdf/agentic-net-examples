using System;
using System.IO;
using System.Text;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber and related types are here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "FirstRows.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Prepare CSV writer (UTF‑8)
                using (StreamWriter writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
                {
                    // Pages are 1‑based in Aspose.Pdf
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Extract tables from the current page
                        TableAbsorber absorber = new TableAbsorber();
                        absorber.Visit(page);

                        // Iterate each detected table (AbsorbedTable)
                        foreach (var table in absorber.TableList)
                        {
                            // Ensure the table has at least one row
                            if (table.RowList != null && table.RowList.Count > 0)
                            {
                                var firstRow = table.RowList[0];
                                var cellValues = new string[firstRow.CellList.Count];

                                // Extract text from each cell of the first row
                                for (int col = 0; col < firstRow.CellList.Count; col++)
                                {
                                    var cell = firstRow.CellList[col];
                                    // Concatenate all text fragments inside the cell
                                    string text = string.Concat(cell.TextFragments.Select(tf => tf.Text));
                                    text = text ?? string.Empty;

                                    // Escape double quotes and commas for CSV compliance
                                    text = text.Replace("\"", "\"\"");
                                    if (text.Contains(",") || text.Contains("\""))
                                        text = $"\"{text}\"";

                                    cellValues[col] = text;
                                }

                                // Write the CSV line
                                writer.WriteLine(string.Join(",", cellValues));
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"First rows of tables saved to '{outputCsv}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
