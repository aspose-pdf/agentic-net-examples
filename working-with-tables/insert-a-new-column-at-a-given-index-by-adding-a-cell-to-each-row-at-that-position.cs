using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int columnIndex = 1; // zero‑based position where the new column will be inserted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Locate the first table in the document
            Table targetTable = null;
            foreach (Page page in doc.Pages)
            {
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is Table tbl)
                    {
                        targetTable = tbl;
                        break;
                    }
                }
                if (targetTable != null) break;
            }

            if (targetTable == null)
            {
                Console.Error.WriteLine("No table found in the document.");
                return;
            }

            // Insert a new cell into each row at the specified column index
            foreach (Row row in targetTable.Rows)
            {
                // Clamp the insertion position to a valid range (0 … Count)
                int insertPos = Math.Min(columnIndex, row.Cells.Count);
                // Create an empty cell
                Cell newCell = new Cell();
                // Insert the cell using Cells.Insert (method from documentation)
                row.Cells.Insert(insertPos, newCell);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Column inserted and saved to '{outputPath}'.");
    }
}