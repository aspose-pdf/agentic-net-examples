using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class ExportTablesInAnnotationRegions
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "ExportedTables";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Counter for naming CSV files
        int csvCounter = 1;

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Collect all SquareAnnotations on the current page
                var squareAnnotations = new List<SquareAnnotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is SquareAnnotation square)
                        squareAnnotations.Add(square);
                }

                if (squareAnnotations.Count == 0)
                    continue; // No regions on this page

                // Extract tables from the page using TableAbsorber
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);
                var tables = absorber.TableList; // IList<AbsorbedTable>

                // For each region, find intersecting tables
                foreach (SquareAnnotation region in squareAnnotations)
                {
                    // Region rectangle
                    Aspose.Pdf.Rectangle regionRect = region.Rect;

                    foreach (var absorbedTable in tables)
                    {
                        // Table rectangle (AbsorbedTable.Rectangle)
                        Aspose.Pdf.Rectangle tableRect = absorbedTable.Rectangle;

                        // Simple rectangle intersection test
                        bool intersects =
                            regionRect.LLX < tableRect.URX && regionRect.URX > tableRect.LLX &&
                            regionRect.LLY < tableRect.URY && regionRect.URY > tableRect.LLY;

                        if (!intersects)
                            continue; // Table does not intersect this region

                        // Export the intersecting table to CSV
                        string csvPath = Path.Combine(outputFolder, $"RegionTable{csvCounter}.csv");
                        using (StreamWriter writer = new StreamWriter(csvPath))
                        {
                            foreach (var row in absorbedTable.RowList)
                            {
                                // Build a CSV line from cell texts
                                string[] cellTexts = new string[row.CellList.Count];
                                for (int i = 0; i < row.CellList.Count; i++)
                                {
                                    var cell = row.CellList[i];
                                    // Concatenate all text fragments inside the cell
                                    string text = string.Join(" ", cell.TextFragments.Select(tf => tf.Text));
                                    // Escape double quotes by doubling them
                                    text = text?.Replace("\"", "\"\"") ?? string.Empty;
                                    // Enclose in quotes if the text contains commas or quotes
                                    if (text.Contains(",") || text.Contains("\""))
                                        text = $"\"{text}\"";
                                    cellTexts[i] = text;
                                }
                                writer.WriteLine(string.Join(",", cellTexts));
                            }
                        }

                        Console.WriteLine($"Exported table to {csvPath}");
                        csvCounter++;
                    }
                }
            }
        }

        Console.WriteLine("Export completed.");
    }
}
