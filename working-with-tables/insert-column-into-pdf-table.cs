using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertColumnExample
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    columnIndex = 2; // zero‑based index where the new column will be inserted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Locate the first Table on the first page
            Table table = null;
            Page firstPage = doc.Pages[1];
            foreach (var paragraph in firstPage.Paragraphs)
            {
                if (paragraph is Table tbl)
                {
                    table = tbl;
                    break;
                }
            }

            if (table == null)
            {
                Console.Error.WriteLine("No table found in the document.");
                return;
            }

            // Insert a new cell into each row at the specified column index
            foreach (Row row in table.Rows)
            {
                // Create a new cell with sample text
                Cell newCell = new Cell();
                newCell.Paragraphs.Add(new TextFragment("New"));

                // Insert the cell at the desired position
                row.Cells.Insert(columnIndex, newCell);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Column inserted and document saved to '{outputPath}'.");
    }
}