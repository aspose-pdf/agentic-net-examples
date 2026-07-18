using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RotateTableCellText
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_table_cell.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document disposal with using)
        using (Document doc = new Document(inputPath))
        {
            // Locate tables on the first page
            TableAbsorber tableAbsorber = new TableAbsorber();
            tableAbsorber.Visit(doc.Pages[1]);

            // Ensure at least one table was found
            if (tableAbsorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the first page.");
                doc.Save(outputPath);
                return;
            }

            // Access the first cell of the first row of the first table
            // (adjust indices as needed for a different cell)
            TextFragment fragment = tableAbsorber.TableList[0]
                                            .RowList[0]
                                            .CellList[0]
                                            .TextFragments[1]; // second fragment (index 1) as in documentation example

            // Rotate the text fragment by 60 degrees
            fragment.TextState.Rotation = 60;

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rotated text in cell: {outputPath}");
    }
}