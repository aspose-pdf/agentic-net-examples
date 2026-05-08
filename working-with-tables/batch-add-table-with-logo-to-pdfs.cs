using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchAddTableWithLogo
{
    static void Main()
    {
        // Resolve input and output folders relative to the executable location.
        // This makes the code work on any OS (Windows, Linux, macOS).
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");
        // Path to the company logo image (PNG, JPG, etc.) – also resolved relative to the base directory.
        string logoPath = Path.Combine(baseDir, "Resources", "logo.png");

        // Validate that the required folders/files exist before proceeding.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder.
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Determine output file path (same name, different folder).
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfFile));

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfFile))
            {
                // Get the first page (Aspose.Pdf uses 1‑based indexing).
                Page page = doc.Pages[1];

                // Create a table that will hold the logo and accompanying text.
                Table table = new Table
                {
                    // Define two columns: first for the logo, second for the text.
                    ColumnWidths = "100 400",
                    // Optional visual styling.
                    Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                    DefaultCellPadding = new MarginInfo(5f, 5f, 5f, 5f)
                };

                // Add a single row to the table.
                var row = table.Rows.Add();

                // ----- Logo cell -----
                var logoCell = row.Cells.Add();
                Image logoImage = new Image { File = logoPath };
                // Optionally set a fixed width/height to fit the cell (adjust as needed).
                // logoImage.FixWidth = 80f; // example
                logoCell.Paragraphs.Add(logoImage);

                // ----- Text cell -----
                var textCell = row.Cells.Add();
                TextFragment companyName = new TextFragment("Acme Corporation");
                companyName.TextState.FontSize = 14;
                companyName.TextState.Font = FontRepository.FindFont("Helvetica");
                companyName.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
                textCell.Paragraphs.Add(companyName);

                // Insert the table at the beginning of the page's content.
                page.Paragraphs.Insert(0, table);

                // Save the modified document to the output location.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(pdfFile)} → {outputPath}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
