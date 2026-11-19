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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to extract tables from the document
            TableAbsorber absorber = new TableAbsorber();

            // Absorb tables from the whole document (or you can call absorber.Visit(doc.Pages[i]) per page)
            absorber.Visit(doc);

            // Iterate over the extracted tables
            Console.WriteLine($"Found {absorber.TableList.Count} tables.");
            int tableIndex = 1;
            foreach (AbsorbedTable absorbed in absorber.TableList)
            {
                // AbsorbedTable provides the page number via PageNum and geometry via Rectangle
                int page = absorbed.PageNum; // NOT PageNumber
                Aspose.Pdf.Rectangle bounds = absorbed.Rectangle;
                Console.WriteLine($"Table {tableIndex}: Page {page}, Rows {absorbed.RowList.Count}, Bounds [{bounds.LLX},{bounds.LLY},{bounds.URX},{bounds.URY}]");
                tableIndex++;
            }
        }
    }
}
