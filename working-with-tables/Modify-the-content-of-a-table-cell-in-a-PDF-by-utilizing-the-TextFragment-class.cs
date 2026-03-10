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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber tableAbsorber = new TableAbsorber();

            // Visit each page to collect tables
            foreach (Page page in doc.Pages)
            {
                tableAbsorber.Visit(page);
            }

            // Verify that at least one table was found
            if (tableAbsorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found in the document.");
                doc.Save(outputPath);
                return;
            }

            // Access the first table, first row, first cell
            AbsorbedCell cell = tableAbsorber.TableList[0].RowList[0].CellList[0];

            // Each cell contains a collection of TextFragment objects (1‑based indexing)
            if (cell.TextFragments.Count > 0)
            {
                // Modify the text of the first fragment in the cell
                TextFragment fragment = cell.TextFragments[1];
                fragment.Text = "New cell content";
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}