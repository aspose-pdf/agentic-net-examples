using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Keyword to search for inside table cells and the replacement text
        const string keyword     = "TARGET";
        const string replacement = "NEW VALUE";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TableAbsorber extracts tables from pages
            TableAbsorber tableAbsorber = new TableAbsorber();

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Perform table extraction on the current page
                tableAbsorber.Visit(doc.Pages[pageNum]);
            }

            // Walk through each extracted table, row, cell and its text fragments
            foreach (var absorbedTable in tableAbsorber.TableList)
            {
                foreach (var row in absorbedTable.RowList)
                {
                    foreach (var cell in row.CellList)
                    {
                        // Each cell may contain multiple TextFragments
                        foreach (var fragment in cell.TextFragments)
                        {
                            if (!string.IsNullOrEmpty(fragment.Text) && fragment.Text.Contains(keyword))
                            {
                                // Replace the entire fragment text with the new value
                                fragment.Text = replacement;
                            }
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}