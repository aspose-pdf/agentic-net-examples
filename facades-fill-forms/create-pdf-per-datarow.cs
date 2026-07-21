using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the generated PDF
        const string outputPdf = "RowsPerPage.pdf";

        // Build a sample DataTable (replace with your actual source)
        DataTable sourceTable = new DataTable("Sample");
        sourceTable.Columns.Add("ID", typeof(int));
        sourceTable.Columns.Add("Name", typeof(string));
        sourceTable.Columns.Add("Value", typeof(string));

        // Populate with example rows
        sourceTable.Rows.Add(1, "Alpha", "100");
        sourceTable.Rows.Add(2, "Beta", "200");
        sourceTable.Rows.Add(3, "Gamma", "300");

        // -----------------------------------------------------------------
        // Create a PDF where each DataRow gets its own page
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            foreach (DataRow row in sourceTable.Rows)
            {
                // Add a new page (1‑based indexing is handled internally)
                Page page = doc.Pages.Add();

                // Prepare a simple text representation of the row
                string rowText = $"ID: {row["ID"]}, Name: {row["Name"]}, Value: {row["Value"]}";

                // Create a TextFragment and add it to the page
                TextFragment tf = new TextFragment(rowText)
                {
                    // Optional styling
                    TextState = { FontSize = 14, ForegroundColor = Aspose.Pdf.Color.Black }
                };
                page.Paragraphs.Add(tf);
            }

            // Save the document (PDF format)
            doc.Save(outputPdf);
        }

        // -----------------------------------------------------------------
        // Verify that the PDF contains a separate page for each DataRow
        // -----------------------------------------------------------------
        using (Document verificationDoc = new Document(outputPdf))
        {
            int expectedPages = sourceTable.Rows.Count;
            int actualPages = verificationDoc.Pages.Count; // 1‑based indexing

            if (actualPages == expectedPages)
            {
                Console.WriteLine($"Verification succeeded: PDF has {actualPages} pages as expected.");
            }
            else
            {
                Console.Error.WriteLine($"Verification failed: Expected {expectedPages} pages but found {actualPages}.");
            }
        }
    }
}