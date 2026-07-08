using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class BatchInsertGraph
{
    static void Main(string[] args)
    {
        // Input folder containing PDFs
        string inputFolder = args.Length > 0 ? args[0] : "InputPdfs";
        // Output folder for processed PDFs
        string outputFolder = args.Length > 1 ? args[1] : "OutputPdfs";
        // Path to the company logo image (PNG, JPG, etc.)
        string logoPath = args.Length > 2 ? args[2] : "logo.png";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Fully qualify System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
            string fileName = System.IO.Path.GetFileName(pdfFile);
            string outputPath = System.IO.Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (lifecycle rule: use using)
                using (Document doc = new Document(pdfFile))
                {
                    // Iterate over all pages (1‑based indexing)
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];

                        // -------------------------------------------------
                        // 1. Create a Graph container (size can be page size)
                        // -------------------------------------------------
                        // Width and height are arbitrary; here we use 200x100 points
                        // Use double literals as the Graph constructor now expects double values
                        Graph graph = new Graph(200.0, 100.0);

                        // -------------------------------------------------
                        // 2. Define a rectangle shape that will represent the logo area
                        // -------------------------------------------------
                        // Position: left=20, bottom=20, width=180, height=80 (inside the graph)
                        Aspose.Pdf.Drawing.Rectangle logoRectShape = new Aspose.Pdf.Drawing.Rectangle(20f, 20f, 180f, 80f);
                        logoRectShape.GraphInfo = new GraphInfo
                        {
                            // Light gray fill, black border, 1 point line width
                            FillColor = Aspose.Pdf.Color.LightGray,
                            Color = Aspose.Pdf.Color.Black,
                            LineWidth = 1f
                        };
                        graph.Shapes.Add(logoRectShape);

                        // -------------------------------------------------
                        // 3. Add the Graph to the page
                        // -------------------------------------------------
                        page.Paragraphs.Add(graph);

                        // -------------------------------------------------
                        // 4. Place the actual logo image inside the rectangle area
                        // -------------------------------------------------
                        // Calculate the absolute position on the page (same as the shape above)
                        // Note: Aspose.Pdf.Rectangle constructor expects (llx, lly, urx, ury)
                        double llx = page.PageInfo.Width - 220; // right‑hand side margin
                        double lly = page.PageInfo.Height - 120; // top‑hand side margin
                        double urx = llx + 200;
                        double ury = lly + 100;

                        Aspose.Pdf.Rectangle logoPageRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                        using (FileStream logoStream = File.OpenRead(logoPath))
                        {
                            // AddImage places the image proportionally inside the rectangle
                            page.AddImage(logoStream, logoPageRect);
                        }
                    }

                    // Save the modified document (lifecycle rule: use Save)
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
