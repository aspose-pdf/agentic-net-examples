using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // TableAbsorber resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "PagesWithTables.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // List to hold page numbers (1‑based) that contain at least one table
        List<int> pagesWithTables = new List<int>();

        // Open the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate using 1‑based indexing as required by Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // TableAbsorber extracts tables from a page
                TableAbsorber tableAbsorber = new TableAbsorber();

                // Visit the current page
                tableAbsorber.Visit(page);

                // If any tables were found, record the page number
                if (tableAbsorber.TableList != null && tableAbsorber.TableList.Count > 0)
                {
                    pagesWithTables.Add(pageIndex);
                }
            }
        }

        // Write the page numbers to the output text file, one per line
        try
        {
            using (StreamWriter writer = new StreamWriter(outputTxtPath, false))
            {
                foreach (int pageNum in pagesWithTables)
                {
                    writer.WriteLine(pageNum);
                }
            }

            Console.WriteLine($"Pages with tables saved to '{outputTxtPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write output file: {ex.Message}");
        }
    }
}