using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextFragment and Position are defined here

class Program
{
    static void Main()
    {
        // Paths for the generated PDF
        const string outputPdf = "RowsPerPage.pdf";

        // Prepare a sample DataTable with some rows
        DataTable table = new DataTable("Sample");
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Rows.Add(1, "Alice");
        table.Rows.Add(2, "Bob");
        table.Rows.Add(3, "Charlie");

        // -----------------------------------------------------------------
        // Create a PDF where each DataTable row occupies its own page
        // -----------------------------------------------------------------
        try
        {
            using (Document doc = new Document())
            {
                // Iterate over rows (0‑based indexing, page numbers are 1‑based internally)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    // Add a new page
                    Page page = doc.Pages.Add();

                    // Build a simple text fragment showing row data
                    string rowText = $"Row {i + 1}:\nId = {table.Rows[i]["Id"]}\nName = {table.Rows[i]["Name"]}";
                    TextFragment tf = new TextFragment(rowText)
                    {
                        // Position the text near the top‑left corner
                        Position = new Position(50, 750)
                    };
                    page.Paragraphs.Add(tf);
                }

                // Save the PDF
                doc.Save(outputPdf);
            }

            // -----------------------------------------------------------------
            // Verify the PDF contains a separate page for each DataTable row
            // Use Document to obtain the page count (PdfFileEditor has no GetPageCount)
            // -----------------------------------------------------------------
            int pdfPageCount;
            using (Document pdfDoc = new Document(outputPdf))
            {
                pdfPageCount = pdfDoc.Pages.Count;
            }

            // Compare counts and output the result
            if (pdfPageCount == table.Rows.Count)
            {
                Console.WriteLine($"Verification succeeded: PDF has {pdfPageCount} pages, matching the {table.Rows.Count} rows.");
            }
            else
            {
                Console.WriteLine($"Verification failed: PDF has {pdfPageCount} pages, but the DataTable has {table.Rows.Count} rows.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
