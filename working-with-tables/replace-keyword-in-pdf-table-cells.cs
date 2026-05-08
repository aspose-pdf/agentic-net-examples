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
        const string keyword = "PLACEHOLDER";   // text to search for inside cells
        const string replacement = "New Value"; // text to replace the keyword with

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber tableAbsorber = new TableAbsorber();

            // Visit each page to collect tables
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                tableAbsorber.Visit(doc.Pages[i]);
            }

            // Iterate over all found tables
            foreach (var absorbedTable in tableAbsorber.TableList)
            {
                // Iterate over rows in the table
                foreach (var row in absorbedTable.RowList)
                {
                    // Iterate over cells in the row
                    foreach (var cell in row.CellList)
                    {
                        // Iterate over text fragments inside the cell
                        foreach (var fragment in cell.TextFragments)
                        {
                            if (!string.IsNullOrEmpty(fragment.Text) && fragment.Text.Contains(keyword))
                            {
                                // Replace the keyword with the new text
                                fragment.Text = fragment.Text.Replace(keyword, replacement);
                            }
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
