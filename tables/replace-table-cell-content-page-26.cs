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
            // Verify the document has at least 26 pages (1‑based indexing)
            if (doc.Pages.Count < 26)
            {
                Console.Error.WriteLine("Document contains fewer than 26 pages.");
                return;
            }

            // Retrieve page 26
            Page page26 = doc.Pages[26];

            // Create a TableAbsorber and process only page 26
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page26);

            // If no tables were detected, simply save the original document
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on page 26.");
                doc.Save(outputPath);
                return;
            }

            // Replace each cell's content with a new text fragment
            foreach (AbsorbedTable absorbedTable in absorber.TableList)
            {
                // RowList contains the rows of the absorbed table
                foreach (var absorbedRow in absorbedTable.RowList)
                {
                    // CellList contains the cells of the row
                    foreach (var absorbedCell in absorbedRow.CellList)
                    {
                        // Remove existing fragments
                        absorbedCell.TextFragments.Clear();

                        // Create a replacement fragment
                        TextFragment fragment = new TextFragment("Replaced content");
                        fragment.TextState.FontSize = 10;
                        fragment.TextState.Font = FontRepository.FindFont("Arial");

                        // Add the new fragment to the cell
                        absorbedCell.TextFragments.Add(fragment);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
