using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const double maxWidthPoints = 150; // maximum column width in points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages and paragraphs to locate tables
            foreach (Page page in doc.Pages)
            {
                for (int i = 1; i <= page.Paragraphs.Count; i++)
                {
                    if (page.Paragraphs[i] is Table table)
                    {
                        // Limit column expansion by setting a default column width
                        table.DefaultColumnWidth = maxWidthPoints.ToString();

                        // If the number of columns is known, you can also set explicit widths:
                        // table.ColumnWidths = $"{maxWidthPoints} {maxWidthPoints} {maxWidthPoints}";
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with limited column widths to '{outputPath}'.");
    }
}