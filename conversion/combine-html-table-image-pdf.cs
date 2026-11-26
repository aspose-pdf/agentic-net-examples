using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for resources
        const string imagePath = "sample.jpg"; // ensure this image exists in the working directory
        const string outputPdf = "combined_output.pdf";

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // ------------------------------------------------------------
            // 1. Add an HTML fragment
            // ------------------------------------------------------------
            string html = "<p style='font-size:14pt;color:#0066CC;'>This is an <b>HTML fragment</b> inside the PDF.</p>";
            HtmlFragment htmlFragment = new HtmlFragment(html);
            page.Paragraphs.Add(htmlFragment);

            // ------------------------------------------------------------
            // 2. Add a table with a header row and a data row
            // ------------------------------------------------------------
            Table table = new Table
            {
                // Define column widths (in points). Adjust as needed.
                ColumnWidths = "150 150",
                // Optional: add a border to the whole table
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                // Optional: default cell padding
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Header row
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add(new TextFragment("Header 1") { TextState = { FontSize = 12, FontStyle = FontStyles.Bold } });
            headerRow.Cells.Add(new TextFragment("Header 2") { TextState = { FontSize = 12, FontStyle = FontStyles.Bold } });

            // Data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add(new TextFragment("Data A"));
            dataRow.Cells.Add(new TextFragment("Data B"));

            // Add the table to the page
            page.Paragraphs.Add(table);

            // ------------------------------------------------------------
            // 3. Add an image from an external file
            // ------------------------------------------------------------
            if (File.Exists(imagePath))
            {
                Image img = new Image
                {
                    // Load the image from file
                    ImageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read),
                    // Optionally set size; if omitted the image keeps its original dimensions
                    // FixWidth = 200,
                    // FixHeight = 150,
                    // Keep the stream open until the document is saved
                    IsApplyResolution = true
                };
                page.Paragraphs.Add(img);
            }
            else
            {
                Console.WriteLine($"Image file not found: {imagePath}");
            }

            // Save the PDF document
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}
