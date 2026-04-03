using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "OutputPdfs";
        // Path to the company logo image (PNG, JPG, etc.)
        const string logoPath = "logo.png";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify logo file exists
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfFilePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Use fully qualified System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
            string fileName   = System.IO.Path.GetFileName(pdfFilePath);
            string outputPath = System.IO.Path.Combine(outputFolder, fileName);

            // Load the PDF document
            using (Document doc = new Document(pdfFilePath))
            {
                // For simplicity, add the graph and logo to the first page
                Page page = doc.Pages[1];

                // -------------------------------------------------
                // 1. Create a Graph container that will hold shapes
                // -------------------------------------------------
                // Width and height of the graph container (in points)
                // Use the double‑based constructor as the float overload is obsolete
                Graph graph = new Graph(200.0, 100.0);

                // -------------------------------------------------
                // 2. Define a rectangle shape that will serve as the logo background
                // -------------------------------------------------
                // Shape constructor: left, bottom, width, height (float values)
                Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray, // background fill
                    Color     = Aspose.Pdf.Color.Black,    // border color
                    LineWidth = 1f                         // border thickness (float literal)
                };
                // Add the rectangle to the graph
                graph.Shapes.Add(rectShape);

                // -------------------------------------------------
                // 3. Add the graph to the page at a desired location
                // -------------------------------------------------
                // Position the graph by setting its margin (optional)
                // Here we place it near the top‑right corner of the page
                graph.Margin = new MarginInfo
                {
                    Left = 400, // distance from left edge
                    Top  = 700  // distance from bottom edge
                };
                page.Paragraphs.Add(graph);

                // -------------------------------------------------
                // 4. Insert the company logo image inside the rectangle area
                // -------------------------------------------------
                using (FileStream imgStream = File.OpenRead(logoPath))
                {
                    // Define the rectangle where the image will be placed.
                    // The coordinates match the graph's position (left, bottom, right, top).
                    Aspose.Pdf.Rectangle imgRect = new Aspose.Pdf.Rectangle(400, 700, 600, 800);
                    page.AddImage(imgStream, imgRect);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }
    }
}
