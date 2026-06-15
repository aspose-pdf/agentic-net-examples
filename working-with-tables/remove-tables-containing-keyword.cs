using System;
using System.IO;
using System.Linq;                     // For ToArray()
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // TableAbsorber and related types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string keyword    = "CONFIDENTIAL";   // Table will be removed if any cell contains this text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Document lifecycle must be wrapped in a using block (see document‑disposal‑with‑using rule)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (see page‑indexing‑one‑based rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create a TableAbsorber for the current page
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables on this page (Visit(Page) method)
                absorber.Visit(page);

                // TableList is modified by Remove, so work on a copy (see TableAbsorber remarks)
                var tables = absorber.TableList.ToArray();

                foreach (AbsorbedTable table in tables)
                {
                    bool containsKeyword = false;

                    // Scan every cell's text fragments for the keyword
                    foreach (var row in table.RowList)
                    {
                        foreach (var cell in row.CellList)
                        {
                            foreach (TextFragment fragment in cell.TextFragments)
                            {
                                if (fragment.Text != null &&
                                    fragment.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    containsKeyword = true;
                                    break;
                                }
                            }
                            if (containsKeyword) break;
                        }
                        if (containsKeyword) break;
                    }

                    // Remove the table if the keyword was found
                    if (containsKeyword)
                    {
                        absorber.Remove(table);
                    }
                }
            }

            // Save the modified PDF (PDF format, no special SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}