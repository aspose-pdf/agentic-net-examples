using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for TableAbsorber and AbsorbedTable

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string csvPath = "TableRectangles.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare the CSV file (overwrite if it exists)
            using (StreamWriter csvWriter = new StreamWriter(csvPath, false))
            {
                // Write CSV header
                csvWriter.WriteLine("PageNumber,LLX,LLY,URX,URY");

                // Pages are 1‑based in Aspose.Pdf
                for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    // TableAbsorber does NOT implement IDisposable, so instantiate directly
                    TableAbsorber absorber = new TableAbsorber();
                    absorber.Visit(doc.Pages[pageNumber]);

                    foreach (AbsorbedTable absorbed in absorber.TableList)
                    {
                        // Page number is stored in AbsorbedTable.PageNum
                        int page = absorbed.PageNum; // should be the same as pageNumber
                        // Bounding rectangle of the detected table
                        Aspose.Pdf.Rectangle rect = absorbed.Rectangle;

                        // Write the coordinates to CSV
                        csvWriter.WriteLine($"{page},{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}");
                    }
                }
            }
        }

        Console.WriteLine($"Table rectangle data saved to '{csvPath}'.");
    }
}
