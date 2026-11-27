using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Needed for TableAbsorber

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
            // Prepare a dictionary to store table counts for each page (1‑based indexing)
            var tablesPerPage = new Dictionary<int, int>();
            for (int i = 1; i <= doc.Pages.Count; i++)
                tablesPerPage[i] = 0;

            // Use TableAbsorber to count tables on each page. This works for both tagged and untagged PDFs.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                var absorber = new TableAbsorber();
                absorber.Visit(doc.Pages[i]);
                tablesPerPage[i] = absorber.TableList.Count;
            }

            // Output the report
            Console.WriteLine("Number of tables per PDF page:");
            for (int i = 1; i <= doc.Pages.Count; i++)
                Console.WriteLine($"Page {i}: {tablesPerPage[i]}");
        }
    }
}
