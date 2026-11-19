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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Check the page rotation using the Rotate property (Rotation enum)
                if (page.Rotate != Rotation.None)
                {
                    Console.WriteLine($"Page {i} is rotated ({page.Rotate}). Scanning for tables...");

                    // TableAbsorber works on a single page; create a fresh instance per page
                    TableAbsorber tableAbsorber = new TableAbsorber();

                    // Absorb tables from the current page using the correct API (Visit)
                    tableAbsorber.Visit(page);

                    // Report found tables
                    if (tableAbsorber.TableList.Count > 0)
                    {
                        for (int t = 0; t < tableAbsorber.TableList.Count; t++)
                        {
                            var absorbedTable = tableAbsorber.TableList[t];
                            // The Rectangle is in page coordinates (lower‑left x,y, upper‑right x,y)
                            Aspose.Pdf.Rectangle rect = absorbedTable.Rectangle;
                            Console.WriteLine($"  Table {t + 1}: LL=({rect.LLX},{rect.LLY}) UR=({rect.URX},{rect.URY})");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No tables detected on this rotated page.");
                    }
                }
                else
                {
                    // Non‑rotated pages are ignored for this specific task
                    Console.WriteLine($"Page {i} is not rotated. Skipping table detection.");
                }
            }
        }
    }
}
