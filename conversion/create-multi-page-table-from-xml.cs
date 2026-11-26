using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input XML and output PDF
        const string xmlPath   = "input.xml";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file into a Document (Aspose.Pdf can parse XML/XDP)
        using (Document doc = new Document(xmlPath))
        {
            // Create a table that is allowed to break across pages
            Table table = new Table
            {
                // Define three equal-width columns (adjust as needed)
                ColumnWidths = "100 100 100",
                // Enable automatic pagination for the table
                IsBroken = true
            };

            // Example: add header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add multiple data rows to demonstrate spanning multiple pages
            for (int i = 1; i <= 50; i++) // 50 rows will force pagination
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} - Col 1");
                row.Cells.Add($"Row {i} - Col 2");
                row.Cells.Add($"Row {i} - Col 3");
            }

            // Add the table to the first page; Aspose.Pdf will create new pages as needed
            doc.Pages[1].Paragraphs.Add(table);

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated and saved to '{pdfPath}'.");
    }
}