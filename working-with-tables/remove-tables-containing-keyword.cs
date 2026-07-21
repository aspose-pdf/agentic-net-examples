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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Create a TableAbsorber to find all tables in the document
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the whole document
                absorber.Visit(doc);

                // Work on a copy of the TableList because Remove() modifies the collection
                var tables = absorber.TableList.ToList();

                foreach (AbsorbedTable table in tables)
                {
                    bool containsKeyword = false;

                    // Iterate over rows and cells to search for the keyword
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

                    // If the keyword is found in any cell, remove the table from the page
                    if (containsKeyword)
                    {
                        absorber.Remove(table);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tables containing \"{keyword}\" have been removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}