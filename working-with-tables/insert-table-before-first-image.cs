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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Process each page (or a specific page if desired)
            foreach (Page page in doc.Pages)
            {
                // Locate the first image in the page's paragraph collection
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
                    // Create a simple 2x2 table
                    Table table = new Table
                    {
                        // Optional visual settings
                        Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                        DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray),
                        DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
                    };

                    // Add two rows with two cells each
                    for (int r = 0; r < 2; r++)
                    {
                        Row row = table.Rows.Add();
                        for (int c = 0; c < 2; c++)
                        {
                            // Each cell contains a TextFragment
                            TextFragment tf = new TextFragment($"R{r + 1}C{c + 1}");
                            row.Cells.Add(tf);
                        }
                    }

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