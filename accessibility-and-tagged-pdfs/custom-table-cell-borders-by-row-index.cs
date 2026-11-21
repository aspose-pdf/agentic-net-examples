using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // for TextFragment if needed
using Aspose.Pdf.Drawing;       // for BorderInfo and BorderSide
using Aspose.Pdf.Annotations;   // not needed but harmless

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a table with 3 columns (adjust as needed)
            Table table = new Table
            {
                // Set column widths (percentage of page width)
                ColumnWidths = "33 33 34",
                // Optional: set default cell padding
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            int totalRows = 6; // example row count; replace with your data source

            for (int rowIndex = 1; rowIndex <= totalRows; rowIndex++)
            {
                // Add a new row to the table
                Row row = table.Rows.Add();

                // Example: three cells per row
                for (int col = 1; col <= 3; col++)
                {
                    // Create cell content
                    Cell cell = row.Cells.Add($"R{rowIndex}C{col}");

                    // Define border style based on the row index
                    // Even rows: thin light gray border
                    // Odd rows: thicker dark gray border
                    if (rowIndex % 2 == 0)
                    {
                        cell.Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.LightGray);
                    }
                    else
                    {
                        cell.Border = new BorderInfo(BorderSide.All, 1.5f, Aspose.Pdf.Color.DarkGray);
                    }

                    // Optional: set background color for visual distinction
                    if (rowIndex % 2 == 0)
                    {
                        cell.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.95, 0.95, 0.95); // light gray fill
                    }
                    else
                    {
                        cell.BackgroundColor = Aspose.Pdf.Color.FromRgb(1, 1, 1); // white fill
                    }
                }
            }

            // Add the table to the first page (or any target page)
            // Ensure the page exists; Aspose.Pdf uses 1‑based indexing
            Page targetPage = doc.Pages[1];
            targetPage.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with custom borders saved to '{outputPath}'.");
    }
}