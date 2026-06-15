using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchAddTableWithLogo
{
    static void Main()
    {
        // Input folder containing PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Output folder for processed PDFs
        const string outputFolder = @"C:\OutputPdfs";
        // Path to the company logo image (any supported format)
        const string logoPath = @"C:\Assets\company_logo.png";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify logo file exists
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document (using statement ensures proper disposal)
                using (Document doc = new Document(pdfFile))
                {
                    // Create a table with two columns: logo and company name
                    Table table = new Table
                    {
                        // Optional styling – use float literals for width values
                        Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                        DefaultCellPadding = new MarginInfo(5f, 5f, 5f, 5f),
                        DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray)
                    };

                    // Define column widths (percentage of page width)
                    // First column for logo (20%), second for text (80%)
                    table.ColumnWidths = "20 80";

                    // Add a single row
                    Row row = table.Rows.Add();

                    // ----- Cell 1: Logo image -----
                    Cell logoCell = row.Cells.Add();
                    // Create an Image object and set its source file
                    Image logoImg = new Image
                    {
                        File = logoPath,
                        // Scale the image to fit the cell (optional)
                        ImageScale = 0.5f // float literal required by Aspose.Pdf
                    };
                    // Add the image to the cell's paragraph collection
                    logoCell.Paragraphs.Add(logoImg);

                    // ----- Cell 2: Company name text -----
                    Cell textCell = row.Cells.Add();
                    TextFragment tf = new TextFragment("Acme Corporation")
                    {
                        // Use cross‑platform Aspose.Pdf.Color
                        TextState = { FontSize = 14, Font = FontRepository.FindFont("Helvetica"), ForegroundColor = Aspose.Pdf.Color.DarkBlue }
                    };
                    textCell.Paragraphs.Add(tf);

                    // Insert the table at the top of the first page
                    Page firstPage = doc.Pages[1];
                    // Optionally set a margin from the top of the page – use float literals
                    table.Margin = new MarginInfo(0f, 0f, 20f, 0f);
                    firstPage.Paragraphs.Add(table);

                    // Build output file path preserving original name
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfFile));

                    // Save the modified document (using the standard Save method)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
