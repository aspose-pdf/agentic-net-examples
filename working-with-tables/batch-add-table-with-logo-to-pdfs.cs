using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class BatchAddTableWithLogo
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";
        // Path to the company logo image (PNG, JPG, etc.)
        const string logoPath = @"C:\Assets\company_logo.png";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build the output file path (same file name, different folder)
            string outputPath = System.IO.Path.Combine(outputFolder, System.IO.Path.GetFileName(pdfFile));

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfFile))
            {
                // Use the first page – adjust as needed (e.g., add to every page)
                Page page = doc.Pages[1];

                // ------------------------------------------------------------
                // Create a table that will hold the logo (and optional text)
                // ------------------------------------------------------------
                Table table = new Table();

                // Optional: set table width to 100% of the page width
                // ColumnWidths can be a string with comma‑separated percentages or absolute values
                table.ColumnWidths = "100";

                // Add a single row
                Row row = table.Rows.Add();

                // Add a single cell to the row
                Cell cell = row.Cells.Add();

                // ------------------------------------------------------------
                // Insert the company logo image into the cell
                // ------------------------------------------------------------
                Image logo = new Image
                {
                    // The Image class does not have a constructor that takes a path,
                    // so set the File property after creation.
                    File = logoPath
                };

                // Add the image to the cell's paragraph collection
                cell.Paragraphs.Add(logo);

                // (Optional) Add a text paragraph next to the logo
                // Uncomment the following lines if you want a caption or title
                // TextFragment txt = new TextFragment("Company Name");
                // txt.TextState.FontSize = 12;
                // txt.TextState.Font = FontRepository.FindFont("Helvetica");
                // cell.Paragraphs.Add(txt);

                // ------------------------------------------------------------
                // Add the table to the page's content
                // ------------------------------------------------------------
                page.Paragraphs.Add(table);

                // Save the modified document to the output folder
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {System.IO.Path.GetFileName(pdfFile)} → {outputPath}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
