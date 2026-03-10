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

            // Check if any tables were found
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found in the document.");
            }
            else
            {
                // Work with the first detected table
                var table = absorber.TableList[0];

                // Note: AbsorbedTable does not expose a PageNumber property in older Aspose.Pdf versions.
                // If you need the page number, you can retrieve it via the underlying page reference.
                // For simplicity, we omit the page number here.
                Console.WriteLine($"Table found, rows: {table.RowList.Count}, columns: {table.RowList[0].CellList.Count}");

                // Iterate through rows and cells to read text fragments
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        string cellText = "";
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }
                        Console.WriteLine($"Row {r + 1}, Column {c + 1}: {cellText}");
                    }
                }

                // Example modification: change the second text fragment in the first cell
                if (table.RowList.Count > 0 &&
                    table.RowList[0].CellList.Count > 0 &&
                    table.RowList[0].CellList[0].TextFragments.Count > 1)
                {
                    TextFragment fragment = table.RowList[0].CellList[0].TextFragments[1];
                    fragment.Text = "Modified text";
                }
            }

            // Save the (potentially modified) PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}
