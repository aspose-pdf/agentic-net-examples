using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a table and add it to the page
            Table table = new Table
            {
                // Optional: set table position and column widths
                Left = 50,
                Top  = 700,
                ColumnWidths = "200"
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create a paragraph (TextFragment) with centered alignment
            TextFragment paragraph = new TextFragment("Centered text in cell")
            {
                // HorizontalAlignment is defined in BaseParagraph
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Add the paragraph to the cell's Paragraphs collection
            cell.Paragraphs.Add(paragraph);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}