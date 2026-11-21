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
                // Create a TableAbsorber to extract tables from the first page
                TableAbsorber absorber = new TableAbsorber();

                // Visit the first page (Aspose.Pdf uses 1‑based indexing)
                absorber.Visit(doc.Pages[1]);

                // Proceed only if at least one table was found
                if (absorber.TableList.Count > 0)
                {
                    // Get the first absorbed table
                    var table = absorber.TableList[0];

                    // Ensure the table has at least one row and one cell
                    if (table.RowList.Count > 0 && table.RowList[0].CellList.Count > 0)
                    {
                        // Get the first cell of the first row
                        var cell = table.RowList[0].CellList[0];

                        // Modify each TextFragment inside the cell
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            // Example modification: prepend a label to the existing text
                            fragment.Text = "Modified: " + fragment.Text;
                        }
                    }
                }

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
