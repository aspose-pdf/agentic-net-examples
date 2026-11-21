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
            // Verify that page 12 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("The document has fewer than 12 pages.");
                return;
            }

            // Create a TableAbsorber and visit page 12
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[12]);

            // If a table was found, replace it with a new table that has the modified cell text
            if (absorber.TableList.Count > 0)
            {
                // The first absorbed table on the page
                AbsorbedTable absorbed = absorber.TableList[0];

                // Build a replacement table (you could copy the original layout if needed)
                Table replacement = new Table();
                // Example: keep the same number of columns as the original (optional)
                int columnCount = absorbed.RowList[0].CellList.Count;
                replacement.ColumnWidths = string.Join(" ", new string[columnCount].Select(_ => "100")); // simple equal widths

                // Add rows/cells – here we copy the original rows but change the first cell text
                foreach (var absRow in absorbed.RowList)
                {
                    var newRow = replacement.Rows.Add();
                    foreach (var absCell in absRow.CellList)
                    {
                        // For the very first cell of the very first row, set new text
                        if (absRow == absorbed.RowList[0] && absCell == absRow.CellList[0])
                        {
                            newRow.Cells.Add("New Cell Text");
                        }
                        else
                        {
                            // Preserve existing text (concatenate all fragments)
                            string cellText = string.Empty;
                            foreach (var frag in absCell.TextFragments)
                                cellText += frag.Text;
                            newRow.Cells.Add(cellText);
                        }
                    }
                }

                // Replace the original table with the new one, preserving its position on the page
                absorber.Replace(doc.Pages[12], absorbed, replacement);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
