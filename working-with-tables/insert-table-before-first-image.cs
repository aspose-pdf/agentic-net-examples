using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Find the first Image element in the page's paragraph collection
                int imageIndex = -1;
                for (int i = 0; i < page.Paragraphs.Count; i++)
                {
                    if (page.Paragraphs[i] is Image)
                    {
                        imageIndex = i;
                        break;
                    }
                }

                // If an image was found, insert a table before it
                if (imageIndex != -1)
                {
                    // Create a simple 2‑column table
                    Table table = new Table
                    {
                        // Set column widths (percentage of page width)
                        ColumnWidths = "50% 50%",
                        // Optional: add a border to make the table visible
                        Border = new BorderInfo(BorderSide.All, 0.5f)
                    };

                    // Add a header row
                    Row header = table.Rows.Add();
                    Cell headerCell1 = header.Cells.Add();
                    headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
                    Cell headerCell2 = header.Cells.Add();
                    headerCell2.Paragraphs.Add(new TextFragment("Header 2"));

                    // Add a data row
                    Row data = table.Rows.Add();
                    Cell dataCell1 = data.Cells.Add();
                    dataCell1.Paragraphs.Add(new TextFragment("Value A"));
                    Cell dataCell2 = data.Cells.Add();
                    dataCell2.Paragraphs.Add(new TextFragment("Value B"));

                    // Insert the table at the image's index, pushing the image forward
                    page.Paragraphs.Insert(imageIndex, table);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted before images and saved to '{outputPath}'.");
    }
}