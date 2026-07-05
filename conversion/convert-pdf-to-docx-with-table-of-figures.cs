using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for Color

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // Step 1: Extract images and build a table of figures
            // -----------------------------------------------------------------
            Table figuresTable = new Table
            {
                // Simple two‑column layout: Figure number and Description
                ColumnWidths = "50 400"
            };

            // Header row
            Row header = figuresTable.Rows.Add();
            header.Cells.Add("Figure");
            header.Cells.Add("Description");
            // Optional: make header bold
            foreach (Cell cell in header.Cells)
            {
                cell.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Color.Black
                };
            }

            int imageIndex = 1;
            // Iterate all pages and images
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a new row for each image
                    Row row = figuresTable.Rows.Add();
                    // Figure number
                    row.Cells.Add($"Figure {imageIndex}");
                    // Description – use alternative text if set, otherwise a placeholder
                    string description = $"Image {imageIndex}";
                    row.Cells.Add(description);
                    imageIndex++;
                }
            }

            // Insert the table at the end of the first page (or any page you prefer)
            if (pdfDoc.Pages.Count >= 1)
            {
                Page firstPage = pdfDoc.Pages[1]; // 1‑based indexing
                firstPage.Paragraphs.Add(figuresTable);
            }

            // -----------------------------------------------------------------
            // Step 2: Convert the enriched PDF to DOCX
            // -----------------------------------------------------------------
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Export as DOCX (correct enum value)
                Format = DocSaveOptions.DocFormat.DocX,
                // Optional: improve bullet detection, etc.
                RecognizeBullets = true
                // Note: Mode/RecognitionMode property has been removed in recent versions
            };

            pdfDoc.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF converted to DOCX with table of figures: {outputDocxPath}");
    }
}
