using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        string inputFolder = "InputPdfs";
        // Folder where modified PDFs will be saved
        string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Retrieve all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            string fileName = System.IO.Path.GetFileName(pdfPath);
            string outputPath = System.IO.Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count == 0)
                    {
                        doc.Pages.Add();
                    }

                    // Use the first page for inserting the graph
                    Page page = doc.Pages[1];

                    // Create a Graph container sized to the page
                    var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                    // Define a rectangle shape that will represent the company logo area
                    // (left, bottom, width, height) – values are in points
                    var logoRect = new Aspose.Pdf.Drawing.Rectangle(
                        50f,   // left
                        750f,  // bottom
                        100f,  // width
                        50f);  // height

                    // Set visual styling via GraphInfo
                    logoRect.GraphInfo = new GraphInfo
                    {
                        FillColor = Aspose.Pdf.Color.LightGray,
                        Color = Aspose.Pdf.Color.Black,
                        LineWidth = 2f
                    };

                    // Add the rectangle to the graph
                    graph.Shapes.Add(logoRect);

                    // Attach the graph to the page
                    page.Paragraphs.Add(graph);

                    // Save the modified PDF to the output folder
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}