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
        const string csvPath   = "LastTable.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TableAbsorber extracts tables from PDF pages using the Visit method
            TableAbsorber absorber = new TableAbsorber();

            // Pages are 1‑based; iterate through all pages and visit each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                absorber.Visit(doc.Pages[i]);
            }

            // Ensure at least one table was found
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found in the document.");
                return;
            }

            // Get the last extracted table
            var lastTable = absorber.TableList[absorber.TableList.Count - 1];

            // Build CSV content from the table rows and cells
            using (StringWriter sw = new StringWriter())
            {
                foreach (var row in lastTable.RowList)
                {
                    bool firstCell = true;
                    foreach (var cell in row.CellList)
                    {
                        if (!firstCell) sw.Write(",");

                        // Concatenate all text fragments inside the cell
                        string text = string.Concat(cell.TextFragments.Select(tf => tf.Text));
                        text = text?.Replace("\"", "\"\"") ?? string.Empty;

                        bool mustQuote = text.Contains(",") || text.Contains("\"") ||
                                         text.Contains("\n") || text.Contains("\r");

                        if (mustQuote)
                            sw.Write($"\"{text}\"");
                        else
                            sw.Write(text);

                        firstCell = false;
                    }
                    sw.WriteLine();
                }

                // Write the CSV string to the output file
                File.WriteAllText(csvPath, sw.ToString());
            }

            Console.WriteLine($"Last table exported to '{csvPath}'.");
        }
    }
}
