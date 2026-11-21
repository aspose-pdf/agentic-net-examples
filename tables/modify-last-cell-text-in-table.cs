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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least 9 pages (1‑based indexing)
                if (doc.Pages.Count < 9)
                {
                    Console.Error.WriteLine("The document contains fewer than 9 pages.");
                    return;
                }

                // Create a TableAbsorber and visit page 9
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(doc.Pages[9]);

                // If no tables were found, inform the user
                if (absorber.TableList.Count == 0)
                {
                    Console.WriteLine("No tables detected on page 9.");
                }
                else
                {
                    // Get the last table on the page
                    AbsorbedTable lastTable = absorber.TableList[absorber.TableList.Count - 1];

                    // Get the last row of that table
                    AbsorbedRow lastRow = lastTable.RowList[lastTable.RowList.Count - 1];

                    // Get the last cell of that row
                    AbsorbedCell lastCell = lastRow.CellList[lastRow.CellList.Count - 1];

                    // Modify the cell's text via its TextFragments collection
                    if (lastCell.TextFragments.Count > 0)
                    {
                        // Change the first fragment (or any you need)
                        lastCell.TextFragments[0].Text = "Modified cell text";
                    }
                    else
                    {
                        // If the cell is empty, add a new TextFragment
                        lastCell.TextFragments.Add(new TextFragment("Modified cell text"));
                    }
                    // No explicit Update() call is required – changes are reflected automatically.
                }

                // Save the updated PDF
                doc.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
