using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdf = "high_dpi_table.pdf";
        const string imagePath1 = "image1.jpg";
        const string imagePath2 = "image2.jpg";

        // Verify image files exist
        if (!File.Exists(imagePath1) || !File.Exists(imagePath2))
        {
            Console.Error.WriteLine("One or more image files not found.");
            return;
        }

        // Keep image streams alive until the document is saved
        var imageStreams = new List<MemoryStream>();

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create a visual Table (Aspose.Pdf.Table) – not the logical structure table
            Table table = new Table
            {
                // Optional: set column widths (in points). Adjust as needed.
                ColumnWidths = "200 200"
            };

            // ------------------------------
            // First row – high‑DPI Image 1
            // ------------------------------
            Row row1 = table.Rows.Add();

            // Load the first image into a memory stream
            byte[] imgBytes1 = File.ReadAllBytes(imagePath1);
            var ms1 = new MemoryStream(imgBytes1);
            imageStreams.Add(ms1);
            Image img1 = new Image
            {
                ImageStream = ms1,
                // Instruct Aspose.Pdf to respect the image's DPI information
                IsApplyResolution = true
            };

            // Add the image to the first cell of the row
            Cell cell1 = row1.Cells.Add();
            cell1.Paragraphs.Add(img1);

            // Second cell in the same row – placeholder text
            Cell cell2 = row1.Cells.Add();
            TextFragment tf = new TextFragment("Image 1 (300 DPI)");
            cell2.Paragraphs.Add(tf);

            // ------------------------------
            // Second row – high‑DPI Image 2
            // ------------------------------
            Row row2 = table.Rows.Add();

            // Load the second image into a memory stream
            byte[] imgBytes2 = File.ReadAllBytes(imagePath2);
            var ms2 = new MemoryStream(imgBytes2);
            imageStreams.Add(ms2);
            Image img2 = new Image
            {
                ImageStream = ms2,
                IsApplyResolution = true
            };

            Cell cell1b = row2.Cells.Add();
            cell1b.Paragraphs.Add(img2);

            // Second cell in the second row – placeholder text
            Cell cell2b = row2.Cells.Add();
            TextFragment tf2 = new TextFragment("Image 2 (300 DPI)");
            cell2b.Paragraphs.Add(tf2);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPdf);
        }

        // Dispose image streams after the document has been saved
        foreach (var ms in imageStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"PDF with high‑DPI images saved to '{outputPdf}'.");
    }
}
