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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Use TableAbsorber to locate tables on the first page
            TableAbsorber tableAbsorber = new TableAbsorber();
            tableAbsorber.Visit(doc.Pages[1]);

            // Verify that at least one table was found
            if (tableAbsorber.TableList.Count > 0)
            {
                // Access the first table, first row, first cell
                var cell = tableAbsorber.TableList[0].RowList[0].CellList[0];

                // Ensure the cell contains text fragments
                if (cell.TextFragments.Count > 0)
                {
                    // Modify the text of the first fragment using TextFragment
                    TextFragment fragment = cell.TextFragments[0];
                    fragment.Text = "New cell content";
                }
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}