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

        using (Document doc = new Document(inputPath))
        {
            // Verify that page 20 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 20)
            {
                Console.Error.WriteLine("The document contains fewer than 20 pages.");
                return;
            }

            // Create a TableAbsorber and visit page 20
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[20]);

            // If no tables were detected, simply save the original document
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on page 20.");
                doc.Save(outputPath);
                return;
            }

            // Work with the first absorbed table (for demonstration)
            AbsorbedTable table = absorber.TableList[0];

            // Replace each cell's content with a new TextFragment
            for (int rowIndex = 0; rowIndex < table.RowList.Count; rowIndex++)
            {
                var row = table.RowList[rowIndex];
                for (int colIndex = 0; colIndex < row.CellList.Count; colIndex++)
                {
                    var cell = row.CellList[colIndex];
                    // Create a fragment containing custom text
                    TextFragment fragment = new TextFragment($"R{rowIndex + 1}C{colIndex + 1}");
                    fragment.TextState.FontSize = 12;
                    fragment.TextState.Font = FontRepository.FindFont("Arial");

                    // Clear existing fragments and add the new one
                    cell.TextFragments.Clear();
                    cell.TextFragments.Add(fragment);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
