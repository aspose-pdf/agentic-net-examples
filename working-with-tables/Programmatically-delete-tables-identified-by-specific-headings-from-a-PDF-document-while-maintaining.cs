using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Headings that identify tables to be removed
    static readonly string[] TargetHeadings = new[]
    {
        "Summary of Results",
        "Financial Overview",
        "Data Table"
    };

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
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // -------------------------------------------------
                // 1. Find all tables on the current page
                // -------------------------------------------------
                TableAbsorber tableAbsorber = new TableAbsorber();
                tableAbsorber.Visit(page);

                // -------------------------------------------------
                // 2. Find all text fragments on the current page
                // -------------------------------------------------
                TextFragmentAbsorber textAbsorber = new TextFragmentAbsorber();
                textAbsorber.Visit(page);
                // TextFragmentAbsorber.TextFragments returns a TextFragmentCollection,
                // which implements IEnumerable<TextFragment>.
                var textFragments = textAbsorber.TextFragments;

                // -------------------------------------------------
                // 3. For each table, check if a target heading appears
                //    directly above it (same page, Y coordinate higher)
                // -------------------------------------------------
                // Collect tables to remove after the loop to avoid
                // modifying the collection while iterating.
                List<AbsorbedTable> tablesToRemove = new List<AbsorbedTable>();

                foreach (AbsorbedTable table in tableAbsorber.TableList)
                {
                    // Bounding rectangle of the table (if available)
                    // Some versions expose Rectangle; otherwise use
                    // the rectangle of the first cell as an approximation.
                    Aspose.Pdf.Rectangle tableRect = table.Rectangle ??
                        (table.RowList.Count > 0 && table.RowList[0].CellList.Count > 0
                            ? table.RowList[0].CellList[0].Rectangle
                            : null);

                    if (tableRect == null)
                        continue; // cannot determine position, skip

                    // Look for a heading text fragment that is above the table
                    bool headingFound = false;
                    foreach (TextFragment tf in textFragments)
                    {
                        // Quick filter: text must match one of the target headings
                        foreach (string heading in TargetHeadings)
                        {
                            if (tf.Text != null && tf.Text.Trim().Equals(heading, StringComparison.OrdinalIgnoreCase))
                            {
                                // Use the fragment's rectangle to obtain its Y coordinate.
                                // Rectangle.LLY gives the lower‑left Y; for a single‑line heading this
                                // is sufficient to approximate the vertical position.
                                double textY = tf.Rectangle?.LLY ?? 0;

                                // Table rectangle Y coordinate is the lower edge (LLY).
                                // In PDF coordinate system, higher Y means higher on page.
                                // Consider the heading "above" if its Y is greater than the table's lower edge
                                // and not too far (e.g., within 100 points).
                                if (textY > tableRect.LLY && textY - tableRect.LLY < 100)
                                {
                                    headingFound = true;
                                    break;
                                }
                            }
                        }
                        if (headingFound) break;
                    }

                    if (headingFound)
                        tablesToRemove.Add(table);
                }

                // -------------------------------------------------
                // 4. Remove identified tables while preserving layout
                // -------------------------------------------------
                foreach (AbsorbedTable tbl in tablesToRemove)
                {
                    // The Remove method updates the page content stream,
                    // effectively deleting the table but leaving surrounding
                    // elements (text, images, etc.) untouched.
                    tableAbsorber.Remove(tbl);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tables with specified headings removed. Output saved to '{outputPath}'.");
    }
}
