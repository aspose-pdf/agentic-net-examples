using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "page_element_counts.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Determine how many pages we will process (max 3 or total pages)
            int pagesToProcess = Math.Min(3, doc.Pages.Count);

            // Prepare CSV file for writing
            using (StreamWriter writer = new StreamWriter(outputCsv, false))
            {
                // Write CSV header
                writer.WriteLine("PageNumber,ElementCount");

                // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                for (int i = 1; i <= pagesToProcess; i++)
                {
                    // Create a new GraphicsAbsorber for each page (belongs to Aspose.Pdf.Vector)
                    using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                    {
                        // Visit the page and collect graphic elements
                        absorber.Visit(doc.Pages[i]);

                        // Get the count of graphic elements on the page
                        int elementCount = absorber.Elements.Count;

                        // Write the result to CSV
                        writer.WriteLine($"{i},{elementCount}");
                    }
                }
            }

            Console.WriteLine($"Element counts written to '{outputCsv}'.");
        }
    }
}