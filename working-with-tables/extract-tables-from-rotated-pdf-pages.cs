using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Check if the page has a rotation applied
                if (page.Rotate != Rotation.None)
                {
                    Console.WriteLine($"Page {i} is rotated ({page.Rotate}). Searching for tables...");

                    // Create a TableAbsorber instance
                    TableAbsorber absorber = new TableAbsorber();

                    // Extract tables from the rotated page
                    absorber.Visit(page);

                    // Output the number of tables found on this page
                    Console.WriteLine($"  Tables found: {absorber.TableList.Count}");

                    // Example: display text from the first cell of each table (if present)
                    foreach (var table in absorber.TableList)
                    {
                        if (table.RowList.Count > 0 && table.RowList[0].CellList.Count > 0)
                        {
                            var cell = table.RowList[0].CellList[0];
                            if (cell.TextFragments.Count > 0)
                            {
                                TextFragment fragment = cell.TextFragments[0];
                                Console.WriteLine($"    First cell text: {fragment.Text}");
                            }
                        }
                    }
                }
                else
                {
                    // Page is not rotated; skip table extraction for this example
                    Console.WriteLine($"Page {i} is not rotated. Skipping table search.");
                }
            }

            // If modifications were made, save the document (optional)
            // doc.Save("output.pdf");
        }
    }
}