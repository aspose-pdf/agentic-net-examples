using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class BatchGraphInserter
{
    static void Main()
    {
        // Paths – adjust as needed
        string inputFolder  = "InputPdfs";
        string outputFolder = "OutputPdfs";
        string graphPath    = "graph.png";   // predefined graph image
        string logoPath     = "logo.png";    // company logo image

        // Validate folders and resources
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        if (!File.Exists(graphPath) || !File.Exists(logoPath))
        {
            Console.Error.WriteLine("Graph or logo image file missing.");
            return;
        }

        // Process each PDF in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Disambiguate System.IO.Path calls
            string fileName   = System.IO.Path.GetFileName(pdfFile);
            string outputPath = System.IO.Path.Combine(outputFolder, fileName);

            try
            {
                // Load PDF – Document must be wrapped in a using block (lifecycle rule)
                using (Document doc = new Document(pdfFile))
                {
                    // Ensure the document has at least one page (page indexing is 1‑based)
                    if (doc.Pages.Count == 0)
                        doc.Pages.Add();

                    // Work with the first page (adjust index if needed)
                    Page page = doc.Pages[1];

                    // -------------------------------------------------
                    // Insert the predefined graph image using ImageStamp
                    // -------------------------------------------------
                    ImageStamp graphStamp = new ImageStamp(graphPath)
                    {
                        // Position using alignment + indents (XPosition/YPosition are not available)
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment   = VerticalAlignment.Bottom,
                        XIndent = 50,   // distance from the left edge
                        YIndent = 500,  // distance from the bottom edge
                        Width   = 500,
                        Height  = 250,
                        Background = false
                    };
                    page.AddStamp(graphStamp);

                    // -------------------------------------------------
                    // Insert a rectangle that will contain the company logo
                    // -------------------------------------------------
                    // Create a Graph container that spans the whole page (size can be page size)
                    Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                    // Define rectangle shape (left, bottom, width, height)
                    Aspose.Pdf.Drawing.Rectangle logoRect = new Aspose.Pdf.Drawing.Rectangle(400, 100, 100, 100);
                    logoRect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;   // background colour
                    logoRect.GraphInfo.Color     = Aspose.Pdf.Color.Black;      // border colour
                    logoRect.GraphInfo.LineWidth = 1;

                    graph.Shapes.Add(logoRect);
                    page.Paragraphs.Add(graph);

                    // -------------------------------------------------
                    // Insert the logo image inside the rectangle using ImageStamp
                    // -------------------------------------------------
                    ImageStamp logoStamp = new ImageStamp(logoPath)
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment   = VerticalAlignment.Bottom,
                        XIndent = 410,   // slightly inside the rectangle (left offset)
                        YIndent = 110,   // slightly above the bottom of the rectangle (bottom offset)
                        Width   = 100,
                        Height  = 100,
                        Background = false
                    };
                    page.AddStamp(logoStamp);

                    // Save the modified PDF – Save() is called inside the using block
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}
