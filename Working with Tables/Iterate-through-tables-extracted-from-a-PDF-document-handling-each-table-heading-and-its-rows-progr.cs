using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Iterate through each detected table
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                AbsorbedTable table = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1} found on page {table.PageNum}");

                // If the table has at least one row, treat the first row as the heading
                if (table.RowList.Count > 0)
                {
                    var headingRow = table.RowList[0];
                    Console.Write("Heading: ");
                    foreach (var cell in headingRow.CellList)
                    {
                        foreach (var fragment in cell.TextFragments)
                        {
                            Console.Write(fragment.Text + " ");
                        }
                    }
                    Console.WriteLine();
                }

                // Process remaining rows (data rows)
                for (int r = 1; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];
                    Console.Write($"Row {r}: ");
                    foreach (var cell in row.CellList)
                    {
                        foreach (var fragment in cell.TextFragments)
                        {
                            Console.Write(fragment.Text + " ");
                        }
                    }
                    Console.WriteLine();
                }

                // Example modification: prepend a marker to the first cell of each row
                foreach (var row in table.RowList)
                {
                    if (row.CellList.Count > 0 && row.CellList[0].TextFragments.Count > 0)
                    {
                        var firstFragment = row.CellList[0].TextFragments[0];
                        firstFragment.Text = "Processed: " + firstFragment.Text;
                    }
                }
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}