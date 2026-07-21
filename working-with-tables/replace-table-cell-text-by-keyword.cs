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
        const string keyword = "TARGET";          // keyword to search for
        const string replacement = "NEW TEXT";    // new cell content

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document (disposed automatically)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create a fresh TableAbsorber for the current page
                TableAbsorber tableAbsorber = new TableAbsorber();
                tableAbsorber.Visit(page);

                // Process each extracted table
                foreach (var absorbedTable in tableAbsorber.TableList)
                {
                    // Iterate rows
                    foreach (var row in absorbedTable.RowList)
                    {
                        // Iterate cells
                        foreach (var cell in row.CellList)
                        {
                            // Each cell may contain multiple TextFragments
                            if (cell.TextFragments != null)
                            {
                                foreach (TextFragment fragment in cell.TextFragments)
                                {
                                    // Replace text if the keyword is found
                                    if (!string.IsNullOrEmpty(fragment.Text) && fragment.Text.Contains(keyword))
                                    {
                                        fragment.Text = replacement;
                                    }
                                }
                            }
                        }
                    }
                }
                // No Reset() needed – a new absorber is created for each page
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
