using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string minWidth   = "50"; // minimum column width in points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Scan page paragraphs for Table objects
                for (int j = 0; j < page.Paragraphs.Count; j++)
                {
                    if (page.Paragraphs[j] is Table table)
                    {
                        // Set a default column width to enforce a minimum width
                        table.DefaultColumnWidth = minWidth;

                        // Optionally, also set explicit column widths if needed
                        // Example: three columns each with the minimum width
                        // table.ColumnWidths = $"{minWidth} {minWidth} {minWidth}";
                    }
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with minimum column width to '{outputPath}'.");
    }
}