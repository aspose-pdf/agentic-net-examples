using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int columnIndexToRemove = 1; // zero‑based index of the column to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Process each page
            foreach (Page page in doc.Pages)
            {
                // Find all tables on the page
                var tables = page.Paragraphs.OfType<Table>().ToList();

                foreach (Table table in tables)
                {
                    // Remove the cell at the specified column index from every row
                    foreach (Row row in table.Rows)
                    {
                        if (columnIndexToRemove >= 0 && columnIndexToRemove < row.Cells.Count)
                        {
                            Cell cell = row.Cells[columnIndexToRemove];
                            row.Cells.Remove(cell); // Cells.Remove method
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Column removed and saved to '{outputPath}'.");
    }
}