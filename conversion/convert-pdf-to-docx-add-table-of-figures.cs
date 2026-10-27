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
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Collect information about each image in the PDF
            var imageInfos = new List<(int PageNumber, XImage Image)>();
            for (int pageIdx = 1; pageIdx <= pdfDoc.Pages.Count; pageIdx++) // 1‑based indexing
            {
                Page page = pdfDoc.Pages[pageIdx];
                foreach (XImage img in page.Resources.Images) // iterate over XImageCollection
                {
                    imageInfos.Add((pageIdx, img));
                }
            }

            // Insert a blank page at the beginning to hold the Table of Figures
            pdfDoc.Pages.Insert(1); // creates an empty page at position 1
            Page tocPage = pdfDoc.Pages[1];

            // Create a table with two columns: Figure number and Description
            Table figuresTable = new Table
            {
                // Set column widths (percentage of page width)
                ColumnWidths = "30 70"
            };

            // Add header row
            Row header = figuresTable.Rows.Add();
            header.Cells.Add("Figure");
            header.Cells.Add("Description");

            // Populate table rows based on extracted images
            int figureCounter = 1;
            foreach (var info in imageInfos)
            {
                Row row = figuresTable.Rows.Add();
                row.Cells.Add($"Figure {figureCounter}");
                row.Cells.Add($"Image extracted from page {info.PageNumber}");
                figureCounter++;
            }

            // Add the table to the newly created page
            tocPage.Paragraphs.Add(figuresTable);

            // Convert the modified PDF to DOCX using explicit DocSaveOptions
            DocSaveOptions docxOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
            };
            pdfDoc.Save(outputDocxPath, docxOptions);
        }

        Console.WriteLine($"PDF converted to DOCX with Table of Figures saved at '{outputDocxPath}'.");
    }
}
