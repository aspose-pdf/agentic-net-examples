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
        const string keyword    = "TARGET";      // text to search for inside cells
        const string replacement = "NEW TEXT";   // new cell content

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Use TableAbsorber to locate tables on the current page
                TableAbsorber tableAbsorber = new TableAbsorber();
                tableAbsorber.Visit(page);

                // Process each detected table
                foreach (AbsorbedTable absorbedTable in tableAbsorber.TableList)
                {
                    // Iterate rows
                    foreach (AbsorbedRow row in absorbedTable.RowList)
                    {
                        // Iterate cells
                        foreach (AbsorbedCell cell in row.CellList)
                        {
                            // Each cell may contain multiple TextFragments
                            foreach (TextFragment fragment in cell.TextFragments)
                            {
                                if (fragment.Text != null && fragment.Text.Contains(keyword))
                                {
                                    // Replace the whole cell content with the new text
                                    cell.TextFragments.Clear();
                                    cell.TextFragments.Add(new TextFragment(replacement));
                                    // No need for IsOverrideByFragment – clearing and adding a new fragment
                                    // automatically overrides the previous content.
                                    break; // exit inner fragment loop – cell already replaced
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cell content replacement completed. Output saved to '{outputPath}'.");
    }
}
