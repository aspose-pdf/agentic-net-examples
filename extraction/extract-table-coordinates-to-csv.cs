using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber lives here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "tables_coordinates.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Use TableAbsorber to locate tables. It returns AbsorbedTable objects that expose
            // PageNum (1‑based) and Rectangle (coordinates) – the correct members for this task.
            TableAbsorber absorber = new TableAbsorber();

            // TableAbsorber.Visit expects a single Page instance, not a PageCollection.
            // Iterate over all pages and feed them to the absorber.
            foreach (Page page in doc.Pages)
            {
                absorber.Visit(page);
            }

            // Prepare CSV file.
            using (StreamWriter writer = new StreamWriter(outputCsvPath))
            {
                // Write CSV header.
                writer.WriteLine("PageIndex,LLX,LLY,URX,URY");

                foreach (AbsorbedTable table in absorber.TableList)
                {
                    // PageNum is 1‑based.
                    int pageIndex = table.PageNum;
                    // Fully‑qualified rectangle type to avoid ambiguity with drawing rectangles.
                    Aspose.Pdf.Rectangle rect = table.Rectangle;

                    writer.WriteLine($"{pageIndex},{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}");
                }
            }

            Console.WriteLine($"Table coordinates have been written to '{outputCsvPath}'.");
        }
    }
}
