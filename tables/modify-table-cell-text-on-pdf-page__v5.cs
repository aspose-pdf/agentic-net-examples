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

        // Open the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document contains at least 35 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 35)
            {
                Console.Error.WriteLine("The document has fewer than 35 pages.");
                return;
            }

            // Access page 35
            Page page35 = doc.Pages[35];

            // Extract tables from the page using TableAbsorber
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page35);

            // If a table was found, modify the text of its first cell
            if (absorber.TableList.Count > 0)
            {
                AbsorbedTable table = absorber.TableList[0];

                if (table.RowList.Count > 0 && table.RowList[0].CellList.Count > 0)
                {
                    AbsorbedCell cell = table.RowList[0].CellList[0];
                    // Clear existing fragments and add a new one with the desired text
                    cell.TextFragments.Clear();
                    cell.TextFragments.Add(new TextFragment("Modified cell text"));
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
