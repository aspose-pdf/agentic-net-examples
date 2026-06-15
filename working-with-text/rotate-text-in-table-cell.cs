using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RotateTableCellText
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Find tables on the first page (adjust page index as needed)
            TableAbsorber tableAbsorber = new TableAbsorber();
            tableAbsorber.Visit(doc.Pages[1]);

            // Ensure at least one table was found
            if (tableAbsorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the page.");
                doc.Save(outputPath); // Save unchanged document
                return;
            }

            // Access the first table, first row, first cell (modify indices as required)
            var cell = tableAbsorber.TableList[0].RowList[0].CellList[0];

            // Rotate each text fragment inside the cell by 60 degrees
            foreach (TextFragment fragment in cell.TextFragments)
            {
                // TextFragmentState.Rotation sets the rotation angle in degrees
                fragment.TextState.Rotation = 60;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rotated text in cell: {outputPath}");
    }
}