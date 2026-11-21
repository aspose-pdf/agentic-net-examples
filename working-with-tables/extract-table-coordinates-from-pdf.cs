using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber resides in this namespace

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
            // TableAbsorber extracts tables from the document
            TableAbsorber absorber = new TableAbsorber();

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Absorb tables on the current page
                absorber.Visit(doc.Pages[pageNum]);

                // Process each extracted table
                for (int i = 0; i < absorber.TableList.Count; i++)
                {
                    // The Rectangle property returns an Aspose.Pdf.Rectangle
                    Aspose.Pdf.Rectangle rect = absorber.TableList[i].Rectangle;

                    // Output the coordinates (lower‑left x/y and upper‑right x/y)
                    Console.WriteLine($"Page {pageNum}, Table {i + 1}:");
                    Console.WriteLine($"  LLX = {rect.LLX}, LLY = {rect.LLY}");
                    Console.WriteLine($"  URX = {rect.URX}, URY = {rect.URY}");
                }

                // Clear the absorber for the next page to avoid mixing results
                absorber.TableList.Clear();
            }
        }
    }
}
