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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from all pages
            absorber.Visit(doc);

            // Work on a copy of the TableList because Remove() changes the collection
            var tables = absorber.TableList.ToArray();

            foreach (var table in tables)
            {
                bool containsKeyword = false;

                // Examine each cell's text fragments for the keyword
                foreach (var row in table.RowList)
                {
                    foreach (var cell in row.CellList)
                    {
                        foreach (var fragment in cell.TextFragments)
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

                // Remove the table if the keyword was found in any cell
                if (containsKeyword)
                {
                    absorber.Remove(table);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}