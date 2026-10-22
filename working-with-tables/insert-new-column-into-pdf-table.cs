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
        const int columnIndex = 2; // zero‑based position where the new column will be inserted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Locate the first table on the first page (adjust as needed for your PDF)
                Table table = null;
                foreach (var paragraph in doc.Pages[1].Paragraphs)
                {
                    if (paragraph is Table tbl)
                    {
                        table = tbl;
                        break;
                    }
                }

                if (table == null)
                {
                    Console.WriteLine("No table found in the document.");
                    return;
                }

                // Insert a new column at the specified index for every row in the table
                foreach (Row row in table.Rows)
                {
                    // Create an empty cell (you can add default content if desired)
                    Cell newCell = new Cell();

                    // Example: add a placeholder space so the cell is not completely empty
                    TextFragment placeholder = new TextFragment(" ");
                    newCell.Paragraphs.Add(placeholder);

                    // Clamp the index to a valid range
                    int insertPos = columnIndex;
                    if (insertPos < 0) insertPos = 0;
                    if (insertPos > row.Cells.Count) insertPos = row.Cells.Count;

                    // Insert the new cell into the row at the desired position
                    row.Cells.Insert(insertPos, newCell);
                }

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Column inserted and PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
