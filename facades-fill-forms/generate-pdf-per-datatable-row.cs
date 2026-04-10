using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths
        const string templatePath = "template.pdf";
        const string outputPath   = "merged_output.pdf";

        // Verify template exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // Build a sample DataTable
        DataTable table = new DataTable("Data");
        table.Columns.Add("FirstName", typeof(string));
        table.Columns.Add("LastName",  typeof(string));
        table.Columns.Add("Email",     typeof(string));

        // Add rows (example data)
        table.Rows.Add("John",  "Doe",   "john.doe@example.com");
        table.Rows.Add("Jane",  "Smith", "jane.smith@example.com");
        table.Rows.Add("Bob",   "Brown", "bob.brown@example.com");

        // Use AutoFiller to generate a merged PDF where each row becomes a page
        AutoFiller filler = new AutoFiller();
        filler.InputFileName = templatePath;

        // Output to a memory stream
        using (MemoryStream mergedStream = new MemoryStream())
        {
            filler.OutputStream = mergedStream;
            filler.ImportDataTable(table);
            filler.Save(); // Generates the PDF into mergedStream

            // Reset stream position for reading
            mergedStream.Position = 0;

            // Save the merged PDF to a file (optional)
            using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                mergedStream.CopyTo(fileOut);
            }

            // Load the generated PDF to verify page count
            using (Document resultDoc = new Document(mergedStream))
            {
                int expectedPages = table.Rows.Count;
                int actualPages   = resultDoc.Pages.Count; // 1‑based indexing

                Console.WriteLine($"Expected pages: {expectedPages}");
                Console.WriteLine($"Actual pages:   {actualPages}");

                if (expectedPages == actualPages)
                {
                    Console.WriteLine("Verification succeeded: one page per DataTable row.");
                }
                else
                {
                    Console.WriteLine("Verification failed: page count does not match row count.");
                }
            }
        }
    }
}