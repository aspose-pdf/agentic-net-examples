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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate tables on the first page
            TableAbsorber tableAbsorber = new TableAbsorber();
            tableAbsorber.Visit(doc.Pages[1]);

            // Ensure at least one table and one cell were found
            if (tableAbsorber.TableList.Count == 0 ||
                tableAbsorber.TableList[0].RowList.Count == 0 ||
                tableAbsorber.TableList[0].RowList[0].CellList.Count == 0)
            {
                Console.Error.WriteLine("No table cell found on the first page.");
                return;
            }

            // Access the first cell of the first table
            AbsorbedCell cell = tableAbsorber.TableList[0].RowList[0].CellList[0];

            // If the cell already contains text fragments, replace the first one;
            // otherwise create a new TextFragment and add it to the cell.
            TextFragment fragment;
            if (cell.TextFragments.Count > 0)
            {
                fragment = cell.TextFragments[0];
                fragment.Text = "Custom Text";
            }
            else
            {
                fragment = new TextFragment("Custom Text");
                // Position the fragment inside the cell (optional precise placement)
                // The Position uses coordinates relative to the page.
                // Here we place it near the top‑left corner of the cell rectangle.
                fragment.Position = new Position(cell.Rectangle.LLX + 5, cell.Rectangle.URY - 15);
                cell.TextFragments.Add(fragment);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated table cell: {outputPath}");
    }
}