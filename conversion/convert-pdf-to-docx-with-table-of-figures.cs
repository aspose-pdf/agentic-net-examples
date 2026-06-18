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

        // Load the source PDF (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a new page that will hold the Table of Figures
            Page tocPage = pdfDoc.Pages.Add();

            // Initialize a table with two columns: Figure number and Description
            Table figuresTable = new Table
            {
                // Define column widths (in points). Adjust as needed.
                ColumnWidths = "50 400"
            };

            // Add header row
            Row header = figuresTable.Rows.Add();
            header.Cells.Add("Figure #");
            header.Cells.Add("Description");

            // Iterate through all pages and their image resources
            int figureIndex = 1;
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a new row for each image
                    Row row = figuresTable.Rows.Add();
                    row.Cells.Add(figureIndex.ToString());

                    // XImage does not expose AlternativeText. Use the image name if available,
                    // otherwise leave the description empty.
                    string description = img.Name ?? string.Empty;
                    row.Cells.Add(description);

                    figureIndex++;
                }
            }

            // Add the completed table to the Table of Figures page
            tocPage.Paragraphs.Add(figuresTable);

            // Prepare DOCX conversion options
            DocSaveOptions docxOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Optional: recognize bullets in the source PDF
                RecognizeBullets = true
            };

            // Save the PDF (now containing the Table of Figures) as DOCX (lifecycle: save)
            pdfDoc.Save(outputDocxPath, docxOptions);
        }

        Console.WriteLine($"Conversion completed. DOCX saved to '{outputDocxPath}'.");
    }
}
