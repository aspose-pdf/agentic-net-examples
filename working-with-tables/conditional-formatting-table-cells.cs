using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class ConditionalFormattingExample
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table
            {
                // Define three equal-width columns
                ColumnWidths = "100 100 100"
            };
            page.Paragraphs.Add(table);

            // Sample numeric data to populate the table
            double[,] data = new double[,] {
                { 30, 75, 120 },
                { 55, 45, 200 }
            };

            // Iterate over the data array, create rows/cells, and apply conditional formatting
            for (int i = 0; i < data.GetLength(0); i++)
            {
                // Add a new row to the table
                Row row = table.Rows.Add();

                for (int j = 0; j < data.GetLength(1); j++)
                {
                    double value = data[i, j];

                    // Add a new cell to the current row
                    Cell cell = row.Cells.Add();

                    // Insert the numeric value as text into the cell
                    TextFragment tf = new TextFragment(value.ToString());
                    cell.Paragraphs.Add(tf);

                    // Conditional background color based on the numeric value
                    if (value > 100)
                    {
                        // Values greater than 100 get a LightGoldenrodYellow background
                        cell.BackgroundColor = Color.LightGoldenrodYellow;
                    }
                    else if (value > 50)
                    {
                        // Values between 51 and 100 get a LemonChiffon background
                        cell.BackgroundColor = Color.LemonChiffon;
                    }
                    // Values 50 or below keep the default background (no color set)
                }
            }

            // Save the PDF with the formatted table – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "conditional_table.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("libgdiplus (GDI+) is required for PDF creation on this platform. " +
                                  "Skipping doc.Save() to avoid TypeInitializationException.");
            }
        }

        Console.WriteLine("Execution completed.");
    }
}