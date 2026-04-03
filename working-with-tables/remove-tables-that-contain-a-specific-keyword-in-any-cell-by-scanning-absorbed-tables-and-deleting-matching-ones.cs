using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // TableAbsorber, AbsorbedTable, AbsorbedCell, TextFragment

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string keyword    = "CONFIDENTIAL";   // keyword to search for (case‑insensitive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber and extract all tables from the document
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc);   // populates absorber.TableList

            // Work on a copy of the TableList because Remove() modifies the collection
            var tables = absorber.TableList.ToList();

            foreach (AbsorbedTable table in tables)
            {
                bool containsKeyword = false;

                // Iterate over rows and cells of the absorbed table
                foreach (var row in table.RowList)
                {
                    foreach (AbsorbedCell cell in row.CellList)
                    {
                        // Each cell may contain multiple text fragments
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

                // If the keyword was found in any cell, remove the whole table from its page
                if (containsKeyword)
                {
                    absorber.Remove(table);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}