using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "graphicCount.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Verify that page 11 exists (Aspose.Pdf uses 1‑based indexing)
                if (doc.Pages.Count < 11)
                {
                    Console.Error.WriteLine("The document contains fewer than 11 pages.");
                    return;
                }

                // Create a GraphicsAbsorber to collect graphic elements (belongs to Aspose.Pdf.Vector)
                using (GraphicsAbsorber graphicsAbsorber = new GraphicsAbsorber())
                {
                    // Visit page 11 – the absorber does not have an overload for Page.Accept
                    Page page = doc.Pages[11];
                    graphicsAbsorber.Visit(page);

                    // Retrieve the total number of graphic elements on the page
                    int graphicCount = graphicsAbsorber.Elements.Count;

                    // Write the count to a text file
                    File.WriteAllText(outputPath, graphicCount.ToString());

                    Console.WriteLine($"Graphic elements on page 11: {graphicCount}");
                    Console.WriteLine($"Count saved to '{outputPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
