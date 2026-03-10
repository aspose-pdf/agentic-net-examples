using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string headingToFind = "Sales Summary"; // the heading that identifies the table

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document (lifecycle rule: use provided Visit method)
            absorber.Visit(doc);

            // Iterate over all found tables
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                var absorbedTable = absorber.TableList[i];

                // Check each cell for the heading text
                bool headingFound = false;
                foreach (var row in absorbedTable.RowList)
                {
                    foreach (var cell in row.CellList)
                    {
                        foreach (var fragment in cell.TextFragments)
                        {
                            if (fragment.Text != null && fragment.Text.Trim().Equals(headingToFind, StringComparison.OrdinalIgnoreCase))
                            {
                                headingFound = true;
                                break;
                            }
                        }
                        if (headingFound) break;
                    }
                    if (headingFound) break;
                }

                if (headingFound)
                {
                    Console.WriteLine($"Table #{i + 1} matches heading \"{headingToFind}\".");
                    // Output the table content (row by row)
                    foreach (var row in absorbedTable.RowList)
                    {
                        foreach (var cell in row.CellList)
                        {
                            // Concatenate all text fragments in the cell
                            string cellText = "";
                            foreach (var fragment in cell.TextFragments)
                            {
                                cellText += fragment.Text;
                            }
                            Console.Write($"{cellText}\t");
                        }
                        Console.WriteLine(); // new line after each row
                    }
                    // If only the first matching table is needed, break here
                    break;
                }
            }
        }
    }
}