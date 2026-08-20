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
        const string keyword    = "TARGET";      // text to search for
        const string newText    = "NEW TEXT";    // replacement text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables on all pages
            TableAbsorber tableAbsorber = new TableAbsorber();

            // Perform the search on the whole document
            tableAbsorber.Visit(doc);

            // Iterate over each detected table
            foreach (var absorbedTable in tableAbsorber.TableList)
            {
                // Iterate over rows
                foreach (var row in absorbedTable.RowList)
                {
                    // Iterate over cells
                    foreach (var cell in row.CellList)
                    {
                        // Each cell may contain one or more TextFragments
                        foreach (var fragment in cell.TextFragments)
                        {
                            // Check if the fragment contains the keyword
                            if (!string.IsNullOrEmpty(fragment.Text) && fragment.Text.Contains(keyword))
                            {
                                // Replace the text
                                fragment.Text = newText;
                                // No need for IsOverrideByFragment – the fragment text replacement is sufficient.
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
