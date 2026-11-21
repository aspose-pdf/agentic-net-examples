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
            // Ensure the document has at least 31 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 31)
            {
                Console.Error.WriteLine("The document does not contain page 31.");
                return;
            }

            // Get page 31
            Page page31 = doc.Pages[31];

            // Create a TableAbsorber and visit the page to extract tables
            TableAbsorber tableAbsorber = new TableAbsorber();
            tableAbsorber.Visit(page31);

            // Check that at least one table was found
            if (tableAbsorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables were detected on page 31.");
            }
            else
            {
                // Retrieve the first absorbed table (adjust index if needed)
                AbsorbedTable absorbedTable = tableAbsorber.TableList[0];

                // Example: modify the text of the first cell in the first row
                if (absorbedTable.RowList.Count > 0 && absorbedTable.RowList[0].CellList.Count > 0)
                {
                    var cell = absorbedTable.RowList[0].CellList[0];
                    // Ensure the cell contains at least one text fragment before modifying
                    if (cell.TextFragments.Count > 0)
                    {
                        cell.TextFragments[0].Text = "Modified cell text";
                    }
                    else
                    {
                        // If no fragments exist, add a new one
                        cell.TextFragments.Add(new TextFragment("Modified cell text"));
                    }
                }

                // If you need to modify other cells, navigate via RowList[i].CellList[j]
                // Example for cell at row 2, column 3 (zero‑based indexes):
                // if (absorbedTable.RowList.Count > 2 && absorbedTable.RowList[2].CellList.Count > 3)
                // {
                //     var otherCell = absorbedTable.RowList[2].CellList[3];
                //     if (otherCell.TextFragments.Count > 0)
                //         otherCell.TextFragments[0].Text = "Another modification";
                // }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
