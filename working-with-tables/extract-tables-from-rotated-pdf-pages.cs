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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Check the rotation of the current page
                Rotation rotation = page.Rotate;
                Console.WriteLine($"Page {i} rotation: {rotation}");

                // Process only pages that have a non‑zero rotation
                if (rotation != Rotation.None)
                {
                    // Create a TableAbsorber for this page
                    TableAbsorber absorber = new TableAbsorber();

                    // Extract tables on the current page
                    absorber.Visit(page);

                    // Output the number of tables found
                    Console.WriteLine($"  Tables found on rotated page {i}: {absorber.TableList.Count}");

                    // Example: iterate tables and print their bounding rectangle
                    foreach (AbsorbedTable table in absorber.TableList)
                    {
                        Aspose.Pdf.Rectangle rect = table.Rectangle;
                        Console.WriteLine($"    Table on page {i} at [{rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY}]");
                    }
                }
            }

            // Save the (potentially unchanged) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}