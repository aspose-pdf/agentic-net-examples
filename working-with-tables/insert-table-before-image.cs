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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Search for an Image object in the page's paragraph collection
                for (int i = 0; i < page.Paragraphs.Count; i++)
                {
                    if (page.Paragraphs[i] is Image)
                    {
                        // Create a simple table to insert
                        Table table = new Table
                        {
                            // Define column widths (in points)
                            ColumnWidths = "150 150",
                            // Table border
                            Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                            // Default cell border (use float literal for line width)
                            DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray)
                        };

                        // Header row
                        Row header = table.Rows.Add();
                        header.Cells.Add("Column 1");
                        header.Cells.Add("Column 2");

                        // Data row
                        Row data = table.Rows.Add();
                        data.Cells.Add("Value 1");
                        data.Cells.Add("Value 2");

                        // Insert the table before the found image (index i)
                        page.Paragraphs.Insert(i, table);

                        // Stop after inserting once per page (optional)
                        break;
                    }
                }
            }

            // Save the modified document (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted before images and saved to '{outputPath}'.");
    }
}
