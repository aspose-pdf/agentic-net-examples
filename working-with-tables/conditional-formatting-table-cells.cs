using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ConditionalFormattingExample
{
    static void Main()
    {
        const string outputPath = "ConditionalFormatting.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with 5 columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100 100 100", // equal column widths
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Add header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Aspose.Pdf.Color.LightGoldenrodYellow;
            header.Cells.Add("Item");
            header.Cells.Add("Q1");
            header.Cells.Add("Q2");
            header.Cells.Add("Q3");
            header.Cells.Add("Q4");

            // Sample data rows
            string[,] data = new string[,] {
                { "Product A", "120", "85",  "95",  "110" },
                { "Product B", "60",  "70",  "55",  "65"  },
                { "Product C", "200", "190", "210", "205" }
            };

            // Populate the table with data
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row row = table.Rows.Add();
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    row.Cells.Add(data[i, j]);
                }
            }

            // Define a numeric threshold
            double threshold = 100.0;

            // Apply conditional formatting: cells with numeric value > threshold get a red background
            // Skip the first column (item names) and the header row
            // NOTE: Aspose.Pdf collections are zero‑based, not one‑based.
            for (int r = 1; r < table.Rows.Count; r++) // start after header (index 0)
            {
                Row row = table.Rows[r];
                for (int c = 1; c < row.Cells.Count; c++) // skip first column (index 0)
                {
                    var cell = row.Cells[c];
                    // The cell's text is stored inside its first paragraph as a TextFragment
                    if (cell.Paragraphs.Count > 0 && cell.Paragraphs[0] is TextFragment tf)
                    {
                        if (double.TryParse(tf.Text, out double value))
                        {
                            if (value > threshold)
                            {
                                // Set background color to light red
                                cell.BackgroundColor = Aspose.Pdf.Color.LightCoral;
                            }
                            else
                            {
                                // Optional: set a different background for values below the threshold
                                cell.BackgroundColor = Aspose.Pdf.Color.LightGreen;
                            }
                        }
                    }
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with conditional formatting saved to '{outputPath}'.");
    }
}
