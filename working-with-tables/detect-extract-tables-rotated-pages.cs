using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Check if the page has a rotation applied
                if (page.Rotate != Rotation.None)
                {
                    Console.WriteLine($"Page {i} is rotated: {page.Rotate}");

                    // Create a TableAbsorber to find tables on this rotated page
                    TableAbsorber absorber = new TableAbsorber();

                    // Extract tables from the current page
                    absorber.Visit(page);

                    // Process each found table (example: output cell count)
                    for (int t = 0; t < absorber.TableList.Count; t++)
                    {
                        var table = absorber.TableList[t];
                        int rowCount = table.RowList.Count;
                        int cellCount = 0;
                        foreach (var row in table.RowList)
                        {
                            cellCount += row.CellList.Count;
                        }

                        Console.WriteLine($"  Table {t + 1}: {rowCount} rows, {cellCount} cells");
                    }
                }
            }

            // Save the (potentially unchanged) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}