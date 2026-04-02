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

        try
        {
            // Load the PDF document (using rule for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Create a TableAbsorber instance
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the entire document
                absorber.Visit(doc);

                // Iterate over all found tables
                int tableIndex = 0;
                foreach (var table in absorber.TableList)
                {
                    Console.WriteLine($"Table {++tableIndex} found on page {table.PageNum}");

                    // Iterate rows within the table
                    int rowIndex = 0;
                    foreach (var row in table.RowList)
                    {
                        Console.WriteLine($"  Row {++rowIndex}");

                        // Iterate cells within the row
                        int cellIndex = 0;
                        foreach (var cell in row.CellList)
                        {
                            Console.WriteLine($"    Cell {++cellIndex} contains {cell.TextFragments.Count} text fragment(s)");

                            // Optionally, output the text of each fragment
                            foreach (var fragment in cell.TextFragments)
                            {
                                Console.WriteLine($"      Text: {fragment.Text}");
                            }
                        }
                    }
                }

                // Save the (potentially modified) document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}