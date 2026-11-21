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
            // Ensure the document has at least three pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Get the third page
            Page pageThree = doc.Pages[3];

            // Use TableAbsorber to locate tables on the page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(pageThree); // extracts tables from the specified page

            // If at least one table was found, delete the first one using absorber.Remove(...)
            if (absorber.TableList.Count > 0)
            {
                AbsorbedTable firstTable = absorber.TableList[0];
                absorber.Remove(firstTable);
                Console.WriteLine("First table on page 3 removed.");
            }
            else
            {
                Console.WriteLine("No tables found on page 3.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
