using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a new page at the end of the document to hold the Table of Figures
            Page tocPage = pdfDoc.Pages.Add();

            // Create a table with two columns: Figure number and Description (placeholder)
            Table figuresTable = new Table
            {
                // Adjust column widths as needed (e.g., 100 points for number, rest for description)
                ColumnWidths = "100 400"
            };

            // Add header row
            Row header = figuresTable.Rows.Add();
            header.Cells.Add("Figure");
            header.Cells.Add("Description");

            // Iterate through all pages and collect images
            int figureIndex = 1;
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // For each image, add a row to the table
                    Row row = figuresTable.Rows.Add();
                    row.Cells.Add($"Figure {figureIndex}");
                    row.Cells.Add($"Image on page {page.Number}"); // placeholder description
                    figureIndex++;
                }
            }

            // Add the table to the new page
            tocPage.Paragraphs.Add(figuresTable);

            // Prepare DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Export as DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Optional: improve bullet recognition, etc.
                RecognizeBullets = true
            };

            // Save the PDF (now containing the Table of Figures) as DOCX
            pdfDoc.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF converted to DOCX with Table of Figures: {outputDocxPath}");
    }
}
