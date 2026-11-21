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

        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 15 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 15)
            {
                Console.Error.WriteLine("The document contains fewer than 15 pages.");
                return;
            }

            // Create a TableAbsorber and visit page 15
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[15]);

            // Copy the detected tables to an array for further processing if needed
            var tablesArray = absorber.TableList.ToArray();
            Console.WriteLine($"Detected {tablesArray.Length} table(s) on page 15.");

            // Remove all detected tables from the document
            foreach (var table in tablesArray)
            {
                absorber.Remove(table);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
