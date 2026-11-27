using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // Added for TableAbsorber and AbsorbedTable

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "RegionTables.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Locate the first SquareAnnotation and obtain its rectangle
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle annotationRect = null;
            int annotationPageNumber = 0; // 1‑based page index

            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is SquareAnnotation square)
                    {
                        // Fully qualified Rectangle to avoid ambiguity
                        annotationRect = new Aspose.Pdf.Rectangle(
                            square.Rect.LLX,
                            square.Rect.LLY,
                            square.Rect.URX,
                            square.Rect.URY);

                        annotationPageNumber = page.Number; // Page.Number is 1‑based
                        break;
                    }
                }
                if (annotationRect != null) break;
            }

            if (annotationRect == null)
            {
                Console.WriteLine("No SquareAnnotation found in the document.");
                return;
            }

            // ------------------------------------------------------------
            // 2. Use TableAbsorber to find all tables (core API, not tagged API)
            // ------------------------------------------------------------
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[annotationPageNumber]); // only tables on the same page

            // Prepare CSV writer
            using (StreamWriter csvWriter = new StreamWriter(csvPath))
            {
                // Write CSV header (optional)
                csvWriter.WriteLine("Page,TableIndex,RowIndex,ColumnIndex,CellText");

                int tableIndex = 0;
                foreach (AbsorbedTable table in absorber.TableList)
                {
                    // --------------------------------------------------------
                    // 3. Determine the page and rectangle of the table
                    // --------------------------------------------------------
                    int tablePageNumber = table.PageNum; // 1‑based page number
                    Aspose.Pdf.Rectangle tableRect = table.Rectangle;

                    // Only consider tables on the same page as the annotation
                    if (tablePageNumber != annotationPageNumber) continue;

                    // Check geometric intersection
                    if (!RectanglesIntersect(annotationRect, tableRect)) continue;

                    tableIndex++;

                    // --------------------------------------------------------
                    // 4. Extract rows and cells from the absorbed table
                    // --------------------------------------------------------
                    int rowIndex = 0;
                    foreach (var row in table.RowList)
                    {
                        rowIndex++;
                        int colIndex = 0;
                        foreach (var cell in row.CellList)
                        {
                            colIndex++;
                            // Concatenate all text fragments inside the cell
                            string cellText = string.Join(" ", cell.TextFragments.Select(tf => tf.Text));
                            cellText = EscapeForCsv(cellText ?? string.Empty);
                            csvWriter.WriteLine($"{tablePageNumber},{tableIndex},{rowIndex},{colIndex},{cellText}");
                        }
                    }
                }
            }

            Console.WriteLine($"Tables intersecting the SquareAnnotation have been saved to '{csvPath}'.");
        }
    }

    // Helper: determine if two rectangles intersect
    static bool RectanglesIntersect(Aspose.Pdf.Rectangle a, Aspose.Pdf.Rectangle b)
    {
        // No overlap if one rectangle is completely to the left/right/top/bottom of the other
        if (a.URX < b.LLX || b.URX < a.LLX) return false;
        if (a.URY < b.LLY || b.URY < a.LLY) return false;
        return true;
    }

    // Helper: escape CSV fields (handles commas and quotes)
    static string EscapeForCsv(string field)
    {
        if (field.Contains('"'))
            field = field.Replace("\"", "\"\"");
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
            field = $"\"{field}\"";
        return field;
    }
}
