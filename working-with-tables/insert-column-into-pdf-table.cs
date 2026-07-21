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
        const int columnIndex = 2; // zero‑based index where the new column will be inserted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Locate the first table on the first page
            Page page = doc.Pages[1];
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
                Console.Error.WriteLine("No table found in the document.");
                return;
            }

            // Insert a new empty cell into each row at the specified column index
            foreach (Row row in table.Rows)
            {
                // Create an empty cell (you can set text or style via TextState if desired)
                Cell newCell = new Cell();

                // Insert the cell at the desired position (Cells.Insert method)
                row.Cells.Insert(columnIndex, newCell);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Column inserted and saved to '{outputPath}'.");
    }
}