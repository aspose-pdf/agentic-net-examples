using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing a table
        const string inputPath = "input.pdf";
        // Output PDF with the new column inserted
        const string outputPath = "output.pdf";
        // Zero‑based index where the new column should be inserted
        const int columnIndex = 2; // insert as the third column

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages to find tables
            foreach (Page page in doc.Pages)
            {
                // Paragraphs collection may contain Table objects
                for (int i = 1; i <= page.Paragraphs.Count; i++)
                {
                    if (page.Paragraphs[i] is Table table)
                    {
                        // For each row in the table, insert a new cell at the desired index
                        foreach (Row row in table.Rows)
                        {
                            // Create an empty cell (you can add a Paragraph/TextFragment here if needed)
                            Cell newCell = new Cell();
                            // Example of adding an empty paragraph (optional)
                            // newCell.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(""));

                            // Insert the cell at the specified column index
                            row.Cells.Insert(columnIndex, newCell);
                        }
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Column inserted at index {columnIndex}. Output saved to '{outputPath}'.");
    }
}
