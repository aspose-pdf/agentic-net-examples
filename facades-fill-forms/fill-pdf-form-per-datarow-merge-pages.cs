using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePdfPath = "template.pdf";   // PDF with form fields
        const string outputPdfPath   = "merged_output.pdf";

        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        // Example DataTable – in real scenario this would come from a DB or other source
        DataTable sourceTable = CreateSampleDataTable();

        // Target document that will contain all generated pages
        using (Document mergedDoc = new Document())
        {
            int rowIndex = 0;

            foreach (DataRow row in sourceTable.Rows)
            {
                // Create a one‑row DataTable for the current record
                DataTable singleRowTable = sourceTable.Clone();
                singleRowTable.ImportRow(row);

                // Use AutoFiller to fill the template with the single row data
                using (AutoFiller filler = new AutoFiller())
                {
                    filler.BindPdf(templatePdfPath);
                    filler.ImportDataTable(singleRowTable);

                    // Save the filled page to a memory stream
                    using (MemoryStream ms = new MemoryStream())
                    {
                        filler.Save(ms);
                        ms.Position = 0; // reset for reading

                        // Load the generated page as a temporary Document
                        using (Document tempDoc = new Document(ms))
                        {
                            // Add the first (and only) page of the temp document to the merged document
                            mergedDoc.Pages.Add(tempDoc.Pages[1]);

                            // Log the processing information
                            rowIndex++;
                            Console.WriteLine($"Processed DataTable row {rowIndex}, created page {mergedDoc.Pages.Count}");
                        }
                    }
                }
            }

            // Save the final merged PDF
            mergedDoc.Save(outputPdfPath);
            Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
        }
    }

    // Helper to create a sample DataTable – replace with real data source as needed
    private static DataTable CreateSampleDataTable()
    {
        DataTable dt = new DataTable("Sample");
        dt.Columns.Add("FirstName", typeof(string));
        dt.Columns.Add("LastName",  typeof(string));
        dt.Columns.Add("Email",     typeof(string));

        dt.Rows.Add("John",  "Doe",  "john.doe@example.com");
        dt.Rows.Add("Jane",  "Smith","jane.smith@example.com");
        dt.Rows.Add("Bob",   "Brown","bob.brown@example.com");

        return dt;
    }
}