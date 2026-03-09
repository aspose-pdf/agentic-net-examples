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

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Locate tables on the first page (TableAbsorber is the correct API)
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[1]);

            // Ensure a table and a cell exist before accessing
            if (absorber.TableList.Count > 0 &&
                absorber.TableList[0].RowList.Count > 0 &&
                absorber.TableList[0].RowList[0].CellList.Count > 0)
            {
                // Access the first cell of the first table
                var cell = absorber.TableList[0].RowList[0].CellList[0];

                // Use an existing TextFragment (or create a new one) for precise formatting
                // Here we modify the first fragment in the cell
                TextFragment fragment = cell.TextFragments[1];
                fragment.Text = "Updated cell text";

                // Apply precise formatting via the TextState of the TextFragment
                fragment.TextState.Font = FontRepository.FindFont("Arial");
                fragment.TextState.FontSize = 12;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with updated table cell saved to '{outputPath}'.");
    }
}