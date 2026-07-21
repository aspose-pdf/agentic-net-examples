using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "styled_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a new table and define column widths
            Table table = new Table
            {
                ColumnWidths = "120 120 120" // three equal columns
            };

            // Add rows and cells, applying visual styles to each cell
            for (int rowIndex = 0; rowIndex < 3; rowIndex++)
            {
                Row row = table.Rows.Add();

                for (int colIndex = 0; colIndex < 3; colIndex++)
                {
                    // Create a cell with sample text
                    Cell cell = row.Cells.Add($"R{rowIndex + 1}C{colIndex + 1}");

                    // Set background color for visual accessibility
                    cell.BackgroundColor = Aspose.Pdf.Color.LightGray;

                    // Define a solid border with thickness 2 and black color using the proper constructor
                    cell.Border = new BorderInfo(BorderSide.All, 2f, Aspose.Pdf.Color.Black);

                    // Apply padding (margin) inside the cell
                    cell.Margin = new MarginInfo(5, 5, 5, 5);
                }
            }

            // Add the styled table to the first page of the document
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with styled table saved to '{outputPath}'.");
    }
}
