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

        // Wrap Document in using for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Check if the page has any rotation applied
                if (page.Rotate != Rotation.None)
                {
                    // Create a TableAbsorber for this page
                    TableAbsorber absorber = new TableAbsorber();

                    // Extract tables on the rotated page
                    absorber.Visit(page);

                    Console.WriteLine($"Page {i} rotated {page.Rotate} – tables found: {absorber.TableList.Count}");

                    // Example modification: prepend a marker to the first text fragment of the first cell
                    if (absorber.TableList.Count > 0)
                    {
                        var firstTable = absorber.TableList[0];
                        if (firstTable.RowList.Count > 0 && firstTable.RowList[0].CellList.Count > 0)
                        {
                            var cell = firstTable.RowList[0].CellList[0];
                            if (cell.TextFragments.Count > 0)
                            {
                                TextFragment fragment = cell.TextFragments[0];
                                fragment.Text = "[Rotated] " + fragment.Text;
                            }
                        }
                    }
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}