using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Vector; // GraphicsAbsorber lives here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "page12_graphics.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 12 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("The document does not contain page 12.");
                return;
            }

            // Get page 12
            Page page12 = doc.Pages[12];

            // Apply GraphicsAbsorber to collect all graphic objects on the page
            using (GraphicsAbsorber absorber = new GraphicsAbsorber())
            {
                // GraphicsAbsorber does not have an overload for Page.Accept; use Visit()
                absorber.Visit(page12);

                // Prepare a dictionary to hold ID → position mapping
                var idToPosition = new Dictionary<string, object>();

                int index = 0;
                foreach (GraphicElement element in absorber.Elements)
                {
                    // Bounds are provided via the Rectangle property
                    Rectangle rect = element.Rectangle;

                    var position = new
                    {
                        llx = rect.LLX,
                        lly = rect.LLY,
                        urx = rect.URX,
                        ury = rect.URY
                    };

                    // GraphicElement does not expose an Id property. Use a stable identifier.
                    // Here we combine the element's hash code with the loop index to minimise collisions.
                    string key = element.GetHashCode().ToString() + "_" + index;
                    idToPosition[key] = position;
                    index++;
                }

                // Serialize the dictionary to pretty‑printed JSON
                string json = JsonSerializer.Serialize(idToPosition, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the output file
                File.WriteAllText(outputJsonPath, json);
            }

            Console.WriteLine($"Graphics positions from page 12 have been saved to '{outputJsonPath}'.");
        }
    }
}
