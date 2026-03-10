using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Loop through each absorbed table
            foreach (AbsorbedTable table in absorber.TableList)
            {
                // Iterate over rows in the current table
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];

                    // Iterate over cells in the current row
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];

                        // Iterate over text fragments inside the cell
                        for (int f = 0; f < cell.TextFragments.Count; f++)
                        {
                            TextFragment fragment = cell.TextFragments[f];

                            // Example processing: prepend table and cell info to the text
                            fragment.Text = $"[T{table.PageNum}-R{r + 1}C{c + 1}] {fragment.Text}";
                        }
                    }
                }
            }

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed tables saved to '{outputPath}'.");
    }
}