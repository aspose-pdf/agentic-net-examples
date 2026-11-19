using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added to resolve TextFragment reference

class BatchTableWithLogo
{
    // Path to the folder containing source PDFs
    private const string InputFolder = @"C:\PdfInput";

    // Path to the folder where processed PDFs will be saved
    private const string OutputFolder = @"C:\PdfOutput";

    // Path to the company logo image (PNG, JPG, etc.)
    private const string LogoPath = @"C:\Assets\company_logo.png";

    static void Main()
    {
        // Ensure output directory exists
        Directory.CreateDirectory(OutputFolder);

        // Verify logo file exists once before processing
        if (!File.Exists(LogoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {LogoPath}");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(InputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfFile);
            string outputPath = Path.Combine(OutputFolder, fileName);

            // Load the PDF, modify it, and save the result
            using (Document doc = new Document(pdfFile))
            {
                AddTableWithLogo(doc, LogoPath);
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }
    }

    // Adds a table containing the company logo to the first page of the document
    private static void AddTableWithLogo(Document doc, string logoPath)
    {
        // Create a table with a single row and two columns
        Table table = new Table
        {
            // Optional visual styling
            Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.LightGray),
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new MarginInfo(10, 10, 10, 10)
        };

        // Add a row to the table
        Row row = table.Rows.Add();

        // ----- Logo cell -----
        Cell logoCell = row.Cells.Add();

        // Load the logo image and place it into the cell
        Image logoImage = new Image();
        // Keep the stream open for the lifetime of the image (Aspose.Pdf copies the data internally)
        logoImage.ImageStream = File.OpenRead(logoPath);
        // Scale the image to fit the cell (optional)
        logoImage.FixWidth = 100; // width in points
        logoImage.FixHeight = 50; // height in points

        logoCell.Paragraphs.Add(logoImage);

        // ----- Text cell -----
        Cell textCell = row.Cells.Add();
        // Add a simple text fragment describing the logo or company name
        TextFragment tf = new TextFragment("Company Name")
        {
            // Use Aspose.Pdf.Color to avoid System.Drawing dependency
            TextState = { FontSize = 14, ForegroundColor = Aspose.Pdf.Color.Black }
        };
        textCell.Paragraphs.Add(tf);

        // Insert the table at the top of the first page
        Page firstPage = doc.Pages[1];
        firstPage.Paragraphs.Insert(0, table);
    }
}
