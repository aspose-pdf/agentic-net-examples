using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string keyword    = "CONFIDENTIAL"; // keyword to search for in any cell

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find all tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Make a copy of the TableList because Remove() modifies the collection
            List<AbsorbedTable> tables = absorber.TableList.ToList();

            // Iterate over each table and check all cells for the keyword
            foreach (AbsorbedTable table in tables)
            {
                bool containsKeyword = false;

                foreach (var row in table.RowList)
                {
                    foreach (var cell in row.CellList)
                    {
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            if (!string.IsNullOrEmpty(fragment.Text) &&
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

                // If the keyword was found, remove the entire table from the page
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