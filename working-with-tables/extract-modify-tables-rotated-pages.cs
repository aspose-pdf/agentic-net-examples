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

        // Open the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Check if the page has any rotation applied
                if (page.Rotate != Rotation.None)
                {
                    Console.WriteLine($"Page {i} is rotated: {page.Rotate}");

                    // Create a TableAbsorber for the current page
                    TableAbsorber absorber = new TableAbsorber();

                    // Extract tables on this rotated page
                    absorber.Visit(page);

                    // Iterate over all tables found on the page
                    foreach (AbsorbedTable table in absorber.TableList)
                    {
                        Console.WriteLine($"  Table found on page {i} at rectangle {table.Rectangle}");
                        Console.WriteLine($"    Rows: {table.RowList.Count}");

                        // Example modification: change text of the first fragment in the first cell
                        if (table.RowList.Count > 0 && table.RowList[0].CellList.Count > 0)
                        {
                            var cell = table.RowList[0].CellList[0];
                            if (cell.TextFragments.Count > 0)
                            {
                                TextFragment fragment = cell.TextFragments[0];
                                fragment.Text = "Modified";
                            }
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