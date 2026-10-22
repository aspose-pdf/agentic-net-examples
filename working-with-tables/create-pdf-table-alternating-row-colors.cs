using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for Color definitions

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Attempt to locate the first Table on the first page
            Table table = doc.Pages[1].Paragraphs
                                 .OfType<Table>()
                                 .FirstOrDefault();

            if (table == null)
            {
                Console.Error.WriteLine("No table found on the first page.");
                return;
            }

            // Iterate over rows and apply alternating background colors
            for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                Row row = table.Rows[rowIndex];
                // Choose background color based on parity of the row index
                Aspose.Pdf.Color bgColor = (rowIndex % 2 == 0)
                                            ? Aspose.Pdf.Color.LightGray   // even rows
                                            : Aspose.Pdf.Color.White;      // odd rows

                // Apply the chosen color to every cell in the current row
                foreach (Cell cell in row.Cells)
                {
                    cell.BackgroundColor = bgColor;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Alternating row colors applied and saved to '{outputPath}'.");
    }
}