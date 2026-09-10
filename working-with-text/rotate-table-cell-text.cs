using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables on the first page
            TableAbsorber tableAbsorber = new TableAbsorber();
            tableAbsorber.Visit(doc.Pages[1]);

            // Verify that at least one table, row, cell, and text fragment exist
            if (tableAbsorber.TableList.Count > 0 &&
                tableAbsorber.TableList[0].RowList.Count > 0 &&
                tableAbsorber.TableList[0].RowList[0].CellList.Count > 0)
            {
                // Access the first cell of the first table
                var cell = tableAbsorber.TableList[0].RowList[0].CellList[0];

                // Rotate each text fragment inside the cell by 60 degrees
                foreach (TextFragment fragment in cell.TextFragments)
                {
                    fragment.TextState.Rotation = 60; // Rotation in degrees
                }
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}