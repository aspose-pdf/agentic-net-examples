using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TableAbsorber and AbsorbedTable

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "tables_dimensions.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Use TableAbsorber to locate tables and obtain their bounding rectangles
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc);

            // Prepare the CSV file (header row)
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
            {
                csvWriter.WriteLine("PageNumber,Width,Height");

                foreach (AbsorbedTable absorbedTable in absorber.TableList)
                {
                    // Page number is 1‑based (AbsorbedTable.PageNum)
                    int pageNumber = absorbedTable.PageNum;

                    // Rectangle provides LLX, LLY, URX, URY (lower‑left and upper‑right corners)
                    Aspose.Pdf.Rectangle rect = absorbedTable.Rectangle;
                    double width = rect.URX - rect.LLX;
                    double height = rect.URY - rect.LLY;

                    csvWriter.WriteLine($"{pageNumber},{width},{height}");
                }
            }
        }

        Console.WriteLine($"Table dimensions have been written to '{outputCsvPath}'.");
    }
}
