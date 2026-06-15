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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Check the rotation of the current page
                Rotation rotation = page.Rotate;
                Console.WriteLine($"Page {i} rotation: {rotation}");

                // Create a TableAbsorber for this page
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the page, regardless of its rotation
                absorber.Visit(page);

                // Example: report how many tables were found on this page
                Console.WriteLine($"  Tables found: {absorber.TableList.Count}");

                // If needed, you can iterate absorber.TableList to inspect or modify tables
                // foreach (var table in absorber.TableList) { /* ... */ }
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}