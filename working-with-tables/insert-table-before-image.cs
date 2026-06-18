using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertTableBeforeImage
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Scan the page's paragraph collection to locate an Image object
                for (int i = 0; i < page.Paragraphs.Count; i++)
                {
                    // Image is a subclass of Aspose.Pdf.Image
                    if (page.Paragraphs[i] is Image)
                    {
                        // Create a simple table to insert before the image
                        Table table = new Table
                        {
                            // Position the table at the top-left of the page (optional)
                            // Left and Top are in points; adjust as needed
                            Left = 50,
                            Top  = (float)page.PageInfo.Height - 100f
                        };

                        // Define two columns
                        table.ColumnWidths = "200 200";

                        // Add a header row
                        Row header = table.Rows.Add();
                        header.Cells.Add("Header 1");
                        header.Cells.Add("Header 2");
                        // Apply a background color to the header (using Aspose.Pdf.Color)
                        header.BackgroundColor = Aspose.Pdf.Color.LightGray;

                        // Add a data row
                        Row data = table.Rows.Add();
                        data.Cells.Add("Cell A1");
                        data.Cells.Add("Cell B1");

                        // Insert the table at the current index (before the image)
                        page.Paragraphs.Insert(i, table);

                        // Since we inserted a new paragraph, the image shifts to i+1.
                        // Break after first insertion per page (remove if multiple images per page are needed)
                        break;
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted before image and saved to '{outputPath}'.");
    }
}