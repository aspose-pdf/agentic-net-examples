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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Sequentially traverse each table found
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                // Access the current table (AbsorbedTable)
                AbsorbedTable table = absorber.TableList[i];

                Console.WriteLine($"Table {i + 1} found on page {table.PageNum}");

                // Iterate through rows of the table
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];

                    // Iterate through cells of the row
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];

                        // Each cell may contain multiple text fragments
                        for (int f = 0; f < cell.TextFragments.Count; f++)
                        {
                            TextFragment fragment = cell.TextFragments[f];
                            Console.WriteLine($"  Cell[{r},{c}] Fragment[{f}] Text: {fragment.Text}");

                            // Example modification: prepend table index to each fragment
                            fragment.Text = $"[T{i + 1}] {fragment.Text}";
                        }
                    }
                }
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}