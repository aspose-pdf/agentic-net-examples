using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string keyword    = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        RemoveTablesContainingKeyword(inputPath, outputPath, keyword);
        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    static void RemoveTablesContainingKeyword(string inputFile, string outputFile, string keyword)
    {
        // Load the PDF document
        using (Document doc = new Document(inputFile))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Work on a copy of the TableList to avoid collection modification issues
            var tables = absorber.TableList.Cast<AbsorbedTable>().ToList();

            foreach (AbsorbedTable table in tables)
            {
                bool shouldRemove = false;

                // Scan all cells of the table for the keyword
                foreach (var row in table.RowList)
                {
                    foreach (var cell in row.CellList)
                    {
                        foreach (var fragment in cell.TextFragments)
                        {
                            if (!string.IsNullOrEmpty(fragment.Text) &&
                                fragment.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                shouldRemove = true;
                                break;
                            }
                        }
                        if (shouldRemove) break;
                    }
                    if (shouldRemove) break;
                }

                // Remove the table if the keyword was found
                if (shouldRemove)
                {
                    absorber.Remove(table);
                }
            }

            // Save the modified document
            doc.Save(outputFile);
        }
    }
}