using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // TableAbsorber extracts tables from the document.
            TableAbsorber absorber = new TableAbsorber();

            // Run the absorber on the whole document (or on specific pages).
            absorber.Visit(doc);

            Console.WriteLine($"Tables found: {absorber.TableList.Count}");

            // Iterate over each absorbed table.
            foreach (AbsorbedTable table in absorber.TableList)
            {
                Console.WriteLine("--- Table found ---");

                int rowIndex = 0;
                // Each table contains rows; iterate them.
                foreach (AbsorbedRow row in table.RowList)
                {
                    rowIndex++;
                    int colIndex = 0;
                    // Each row contains cells; iterate them.
                    foreach (AbsorbedCell cell in row.CellList)
                    {
                        colIndex++;
                        // A cell does not expose a direct Text property; its content is a collection of TextFragments.
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            Console.WriteLine($"Cell [{rowIndex},{colIndex}]: {fragment.Text}");
                        }
                    }
                }
            }
        }
    }
}
