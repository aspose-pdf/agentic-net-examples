using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tables_info.txt";

        // Verify that the input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Create a TableAbsorber to find tables (text-related API lives in Aspose.Pdf.Text)
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the whole document (one-based page indexing is handled internally)
                absorber.Visit(doc);

                // Write information about each discovered table to a text file
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    writer.WriteLine($"Total tables found: {absorber.TableList.Count}");

                    for (int i = 0; i < absorber.TableList.Count; i++)
                    {
                        var table = absorber.TableList[i];
                        writer.WriteLine($"Table {i + 1} on page {table.PageNum}");
                        writer.WriteLine($"  Rows: {table.RowList.Count}");

                        // Iterate rows and cells to output cell text
                        for (int r = 0; r < table.RowList.Count; r++)
                        {
                            var row = table.RowList[r];
                            writer.Write($"    Row {r + 1}: ");

                            for (int c = 0; c < row.CellList.Count; c++)
                            {
                                var cell = row.CellList[c];
                                // Concatenate all text fragments inside the cell
                                string cellText = string.Empty;
                                foreach (var fragment in cell.TextFragments)
                                {
                                    cellText += fragment.Text;
                                }
                                writer.Write($"[{c + 1}] \"{cellText}\" ");
                            }
                            writer.WriteLine();
                        }
                    }
                }

                Console.WriteLine($"Table extraction completed. Details saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}