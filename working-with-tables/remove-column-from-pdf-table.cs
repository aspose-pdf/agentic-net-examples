using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Index of the column to remove (zero‑based)
        const int columnIndexToRemove = 2;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Assume the table is on the first page; adjust as needed
            Page page = doc.Pages[1];

            // Locate the first Table object on the page
            Table table = null;
            foreach (var paragraph in page.Paragraphs)
            {
                if (paragraph is Table t)
                {
                    table = t;
                    break;
                }
            }

            if (table == null)
            {
                Console.WriteLine("No table found on the first page.");
            }
            else
            {
                // Iterate over each row and delete the cell at the specified column index
                foreach (Row row in table.Rows)
                {
                    // Ensure the column index is within the current row's cell count
                    if (columnIndexToRemove >= 0 && columnIndexToRemove < row.Cells.Count)
                    {
                        // Retrieve the cell to be removed
                        var cellToRemove = row.Cells[columnIndexToRemove];

                        // Remove the cell from the row's cell collection
                        row.Cells.Remove(cellToRemove);
                    }
                }

                Console.WriteLine($"Column {columnIndexToRemove} removed from the table.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}