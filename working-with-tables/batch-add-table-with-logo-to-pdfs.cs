using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // Required for Image class

class BatchAddTableWithLogo
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";
        // Path to the company logo image (PNG, JPEG, etc.)
        const string logoPath = @"C:\Resources\company_logo.png";

        // Validate paths
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Disambiguate System.IO.Path from Aspose.Pdf.Drawing.Path
            string fileName = System.IO.Path.GetFileName(pdfFile);
            string outputPath = System.IO.Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfFile))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count == 0)
                    {
                        // Add a blank page if none exist
                        doc.Pages.Add();
                    }

                    // Use the first page (1‑based indexing) to insert the table
                    Page page = doc.Pages[1];

                    // Create a table with two columns
                    Table table = new Table
                    {
                        // Optional: set border for visual clarity
                        Border = new BorderInfo(BorderSide.All, 0.5f),
                        // Define column widths (percentage of page width)
                        ColumnWidths = "30% 70%"
                    };

                    // Add a single row to the table
                    Row row = table.Rows.Add();

                    // ----- Logo cell -----
                    Cell logoCell = row.Cells.Add();
                    // Load the logo image from file stream
                    using (FileStream logoStream = File.OpenRead(logoPath))
                    {
                        Image logoImage = new Image
                        {
                            ImageStream = logoStream,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        logoCell.Paragraphs.Add(logoImage);
                    }

                    // ----- Text cell -----
                    Cell textCell = row.Cells.Add();
                    TextFragment tf = new TextFragment("Company Name")
                    {
                        TextState = { FontSize = 14, FontStyle = FontStyles.Bold }
                    };
                    textCell.Paragraphs.Add(tf);

                    // Insert the table at the top of the page
                    page.Paragraphs.Add(table);

                    // Save the modified PDF to the output folder
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
