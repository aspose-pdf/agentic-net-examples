using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // needed for TextFragment and FontRepository

class BatchAddTableWithLogo
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";
        // Path to the company logo image (PNG, JPG, etc.)
        const string logoPath = @"C:\Assets\company_logo.png";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Load the PDF document (using the standard load constructor)
            using (Document doc = new Document(pdfFile))
            {
                // Create a table with two columns (logo + description)
                Table table = new Table
                {
                    // Optional: set table position on the page
                    Left = 50,
                    Top = 700,
                    // Optional: set column widths (percentage of page width)
                    ColumnWidths = "100 300"
                };

                // Add a row to the table
                var row = table.Rows.Add();

                // First cell: company logo
                var logoCell = row.Cells.Add();
                // Create the image object and set its source file
                var logoImage = new Image
                {
                    File = logoPath
                };
                // Scale the image to fit the cell (optional)
                logoImage.FixWidth = 80;
                logoImage.FixHeight = 80;
                // Add the image to the cell's paragraph collection
                logoCell.Paragraphs.Add(logoImage);

                // Second cell: descriptive text
                var textCell = row.Cells.Add();
                var tf = new TextFragment("Company Name\nAddress Line 1\nAddress Line 2")
                {
                    TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
                };
                textCell.Paragraphs.Add(tf);

                // Add the table to the first page of the document
                Page firstPage = doc.Pages[1];
                firstPage.Paragraphs.Add(table);

                // Determine output file path (preserve original name)
                string outputPath = System.IO.Path.Combine(outputFolder, System.IO.Path.GetFileName(pdfFile));

                // Save the modified document (standard Save method)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {System.IO.Path.GetFileName(pdfFile)}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
