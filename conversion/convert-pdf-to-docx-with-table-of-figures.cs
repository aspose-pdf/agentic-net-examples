using System;
using System.Collections.Generic;
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
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Collect image information from the PDF
            var figures = new List<(int Index, string Name, string AltText)>();
            int figureCounter = 1;

            foreach (Page page in pdfDoc.Pages)
            {
                // Iterate over image resources on the page. XImage does not expose an AlternativeText property
                // in the core API, so we use a placeholder description when none is available.
                foreach (XImage img in page.Resources.Images)
                {
                    // Use a generic description because XImage lacks AlternativeText.
                    string alt = "No description";

                    // XImage.Name may be null; generate a name if needed.
                    string imgName = !string.IsNullOrEmpty(img.Name) ? img.Name : $"Image_{figureCounter}";

                    figures.Add((figureCounter, imgName, alt));
                    figureCounter++;
                }
            }

            // Add a new page at the end to host the Table of Figures
            Page tocPage = pdfDoc.Pages.Add();

            // Create a table with two columns: Figure number and Description
            Table table = new Table
            {
                // Adjust column widths as needed (percentage or absolute units)
                ColumnWidths = "80 420"
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Figure");
            header.Cells.Add("Description");
            // Optional: make header bold
            foreach (Cell cell in header.Cells)
            {
                cell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    Font = FontRepository.FindFont("Helvetica"),
                    ForegroundColor = Aspose.Pdf.Color.Black
                };
            }

            // Data rows for each extracted image
            foreach (var fig in figures)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Figure {fig.Index}");
                row.Cells.Add(fig.AltText);
            }

            // Add the table to the new page
            tocPage.Paragraphs.Add(table);

            // Prepare DOCX save options
            DocSaveOptions docxOptions = new DocSaveOptions
            {
                // Export as DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Use Flow mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,
                // Enable bullet recognition (optional)
                RecognizeBullets = true
            };

            // Save the PDF (now containing the Table of Figures) as DOCX
            pdfDoc.Save(outputDocxPath, docxOptions);
        }

        Console.WriteLine($"Conversion completed. DOCX saved to '{outputDocxPath}'.");
    }
}
