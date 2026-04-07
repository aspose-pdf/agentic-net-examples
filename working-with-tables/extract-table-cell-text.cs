using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file (must exist in the working directory)
        string inputPath = "input.pdf";
        // Output text file that will contain one line per cell
        string outputPath = "output.txt";

        // Verify that the input file exists before trying to open it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: The file '{inputPath}' was not found. Please place the PDF in the working directory or provide a correct path.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc);

            // List to hold extracted cell texts
            List<string> cellTexts = new List<string>();

            // Iterate over all found tables
            foreach (AbsorbedTable table in absorber.TableList)
            {
                // Iterate over rows of the current table
                foreach (AbsorbedRow row in table.RowList)
                {
                    // Iterate over cells of the current row
                    foreach (AbsorbedCell cell in row.CellList)
                    {
                        // AbsorbedCell does not expose a direct Text property.
                        // Retrieve the text by concatenating all TextFragments belonging to the cell.
                        StringBuilder sb = new StringBuilder();
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            sb.Append(fragment.Text);
                        }
                        string cellText = sb.ToString();
                        cellTexts.Add(cellText);
                    }
                }
            }

            // Write each cell's text on a separate line
            File.WriteAllLines(outputPath, cellTexts);
            Console.WriteLine($"Extraction complete. {cellTexts.Count} cells written to '{outputPath}'.");
        }
    }
}
