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

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 27 pages (Aspose.Pdf uses 1‑based indexing).
            if (doc.Pages.Count < 27)
            {
                Console.Error.WriteLine("The document does not contain page 27.");
                return;
            }

            // Get page 27.
            Page page = doc.Pages[27];

            // Use TableAbsorber to extract tables from the page.
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page); // <-- correct method is Visit, not Accept

            if (absorber.TableList.Count > 0)
            {
                // Retrieve the first absorbed table.
                AbsorbedTable absorbedTable = absorber.TableList[0];

                // Build a replacement table that contains the original content plus a new cell.
                Table newTable = new Table();

                // Determine the maximum number of cells in any row of the absorbed table.
                int maxCells = 0;
                foreach (var r in absorbedTable.RowList)
                {
                    if (r.CellList.Count > maxCells)
                        maxCells = r.CellList.Count;
                }

                // Copy each row and its cells.
                foreach (var row in absorbedTable.RowList)
                {
                    var newRow = newTable.Rows.Add();
                    foreach (var cell in row.CellList)
                    {
                        // Concatenate all text fragments of the absorbed cell.
                        string cellText = string.Empty;
                        foreach (var frag in cell.TextFragments)
                            cellText += frag.Text;
                        newRow.Cells.Add(cellText);
                    }
                    // Pad the row with empty cells if it had fewer cells than the maximum.
                    for (int i = row.CellList.Count; i < maxCells; i++)
                        newRow.Cells.Add(string.Empty);
                }

                // Add a new cell with the desired text to the first row (or create a row if none exist).
                if (newTable.Rows.Count > 0)
                {
                    newTable.Rows[0].Cells.Add("New Cell Text");
                }
                else
                {
                    var r = newTable.Rows.Add();
                    r.Cells.Add("New Cell Text");
                }

                // Replace the original table with the newly built one at the same position.
                absorber.Replace(page, absorbedTable, newTable);
            }
            else
            {
                Console.WriteLine("No tables were detected on page 27.");
            }

            // Save the (potentially modified) PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
