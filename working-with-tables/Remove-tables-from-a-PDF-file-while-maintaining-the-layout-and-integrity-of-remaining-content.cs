using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_tables.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // Process each page (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Find tables on the current page
                    TableAbsorber absorber = new TableAbsorber();
                    absorber.Visit(page);

                    // Copy the list to avoid modifying the collection while iterating
                    List<AbsorbedTable> tables = new List<AbsorbedTable>(absorber.TableList);

                    // Remove each detected table
                    foreach (AbsorbedTable table in tables)
                    {
                        absorber.Remove(table);
                    }
                }

                // Clean up any unused resources after table removal
                doc.OptimizeResources();

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tables removed successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}