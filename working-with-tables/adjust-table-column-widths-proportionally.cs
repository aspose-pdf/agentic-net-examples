using System;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // Create a sample PDF with a simple table if it does not exist
        // ------------------------------------------------------------
        if (!System.IO.File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                // Add a page
                Page page = seed.Pages.Add();

                // Create a table with three columns
                Table table = new Table
                {
                    // Initial absolute widths (points). This will be converted to percentages.
                    ColumnWidths = "120 80 100",
                    // Optional: set border for visibility
                    Border = new BorderInfo(BorderSide.All, 0.5f)
                };

                // Add a header row
                Row header = table.Rows.Add();
                header.Cells.Add("Header 1");
                header.Cells.Add("Header 2");
                header.Cells.Add("Header 3");

                // Add a data row
                Row data = table.Rows.Add();
                data.Cells.Add("Cell A");
                data.Cells.Add("Cell B");
                data.Cells.Add("Cell C");

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the seed PDF
                seed.Save(inputPath);
            }
        }

        // ------------------------------------------------------------
        // Load the PDF and adjust column widths proportionally
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is Table table)
                    {
                        string widthsStr = table.ColumnWidths;
                        if (string.IsNullOrWhiteSpace(widthsStr))
                            continue; // No widths defined, skip

                        string[] tokens = widthsStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        double[] values = new double[tokens.Length];
                        double total = 0;

                        for (int i = 0; i < tokens.Length; i++)
                        {
                            string token = tokens[i].TrimEnd('%');
                            if (double.TryParse(token, out double val))
                            {
                                values[i] = val;
                                total += val;
                            }
                            else
                            {
                                values[i] = 0;
                            }
                        }

                        if (total <= 0)
                            continue; // Avoid division by zero

                        List<string> percentWidths = new List<string>();
                        foreach (double val in values)
                        {
                            double pct = (val / total) * 100.0;
                            percentWidths.Add(pct.ToString("0.##") + "%");
                        }

                        table.ColumnWidths = string.Join(" ", percentWidths);
                        table.ColumnAdjustment = Aspose.Pdf.ColumnAdjustment.AutoFitToWindow;
                    }
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Proportional column widths applied and saved to '{outputPath}'.");
    }
}
