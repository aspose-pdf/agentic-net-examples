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
        const int columnIndex = 1; // zero‑based index where the new column will be inserted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Locate the first table on the page
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
                Console.Error.WriteLine("No table found on the first page.");
                return;
            }

            // Insert a new cell into each row at the specified column index
            foreach (Row row in table.Rows)
            {
                // Create a new empty cell (you can set default text/state if desired)
                Cell cell = new Cell
                {
                    // Set a default text state for the new column
                    DefaultCellTextState = new TextState
                    {
                        Font = FontRepository.FindFont("Helvetica"),
                        FontSize = 12,
                        ForegroundColor = Color.Black
                    }
                };

                // Insert the cell at the desired position
                row.Cells.Insert(columnIndex, cell);
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Column inserted and saved to '{outputPath}'.");
    }
}
