using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class BatchAddTableWithLogo
{
    static void Main()
    {
        const string sourceFolder = @"C:\PdfInput";
        const string outputFolder = @"C:\PdfOutput";
        const string logoPath = @"C:\Assets\CompanyLogo.png";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the source folder
        foreach (string pdfFile in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            // Load the PDF document
            using (Document doc = new Document(pdfFile))
            {
                // Create a table with two columns (logo + text)
                Table table = new Table();
                table.ColumnWidths = "100 400"; // widths in points

                // Add a single row
                var row = table.Rows.Add();

                // ----- Cell 1: Company Logo -----
                var logoCell = row.Cells.Add();
                Aspose.Pdf.Image logoImage = new Aspose.Pdf.Image
                {
                    File = logoPath,
                    FixWidth = 80,
                    FixHeight = 80
                };
                logoCell.Paragraphs.Add(logoImage);

                // ----- Cell 2: Company Name -----
                var textCell = row.Cells.Add();
                TextFragment companyName = new TextFragment("Acme Corporation");
                // TextState is read‑only; configure its members instead of reassigning
                companyName.TextState.FontSize = 14;
                companyName.TextState.Font = FontRepository.FindFont("Helvetica");
                companyName.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                textCell.Paragraphs.Add(companyName);

                // Insert the table at the top of the first page
                doc.Pages[1].Paragraphs.Add(table);

                // Build the output file path (same name, different folder)
                string outputPath = System.IO.Path.Combine(outputFolder, System.IO.Path.GetFileName(pdfFile));

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {System.IO.Path.GetFileName(pdfFile)}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
