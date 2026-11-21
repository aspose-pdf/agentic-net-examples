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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new table and configure its properties
            Table table = new Table
            {
                // Adjust column widths to fit the content of each cell
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                // Repeat the first row (header) on each page if the table spans multiple pages
                RepeatingRowsCount = 1
            };

            // ----- Header row (will be repeated) -----
            Row headerRow = table.Rows.Add();
            // First header cell
            Cell headerCell1 = headerRow.Cells.Add();
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            // Second header cell
            Cell headerCell2 = headerRow.Cells.Add();
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));

            // ----- Data rows -----
            for (int i = 1; i <= 5; i++)
            {
                Row dataRow = table.Rows.Add();

                Cell dataCell1 = dataRow.Cells.Add();
                dataCell1.Paragraphs.Add(new TextFragment($"Row {i} - Col 1"));

                Cell dataCell2 = dataRow.Cells.Add();
                dataCell2.Paragraphs.Add(new TextFragment($"Row {i} - Col 2"));
            }

            // Add the table to the first page of the document
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPath}'.");
    }
}
