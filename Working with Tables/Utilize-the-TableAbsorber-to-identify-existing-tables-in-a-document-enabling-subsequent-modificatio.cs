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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // If tables were found, demonstrate a simple modification
            if (absorber.TableList.Count > 0)
            {
                // Get the first absorbed table
                AbsorbedTable absorbedTable = absorber.TableList[0];

                // Ensure the table has at least one row and one cell
                if (absorbedTable.RowList.Count > 0 && absorbedTable.RowList[0].CellList.Count > 0)
                {
                    // Access the first cell's text fragments
                    var cell = absorbedTable.RowList[0].CellList[0];

                    // Modify the second text fragment if it exists (as in the documentation example)
                    if (cell.TextFragments.Count > 1)
                    {
                        TextFragment fragment = cell.TextFragments[1];
                        fragment.Text = "Modified text";
                    }
                }

                // Example of replacing the absorbed table with a new (empty) Table object
                Table replacementTable = new Table();
                absorber.Replace(doc.Pages[absorbedTable.PageNum], absorbedTable, replacementTable);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}