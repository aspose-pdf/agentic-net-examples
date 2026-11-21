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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 32 pages (1‑based indexing)
            if (doc.Pages.Count < 32)
            {
                Console.Error.WriteLine("Document does not contain page 32.");
                return;
            }

            // Create a TableAbsorber and visit page 32
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[32]);

            // Check if any tables were detected on the page
            if (absorber.TableList.Count == 0)
            {
                Console.Error.WriteLine("No table found on page 32.");
                return;
            }

            // For this example we work with the first absorbed table
            AbsorbedTable absorbedTable = absorber.TableList[0];

            // Iterate over rows and cells, replace each cell's content with a new fragment
            int rowIndex = 0;
            foreach (var row in absorbedTable.RowList)
            {
                int colIndex = 0;
                foreach (var cell in row.CellList)
                {
                    // Remove any existing fragments
                    cell.TextFragments.Clear();

                    // Create a new text fragment for the cell
                    TextFragment fragment = new TextFragment($"R{rowIndex + 1}C{colIndex + 1}");
                    fragment.TextState.FontSize = 10;
                    fragment.TextState.Font = FontRepository.FindFont("Arial");

                    // Add the fragment to the cell
                    cell.TextFragments.Add(fragment);

                    colIndex++;
                }
                rowIndex++;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
