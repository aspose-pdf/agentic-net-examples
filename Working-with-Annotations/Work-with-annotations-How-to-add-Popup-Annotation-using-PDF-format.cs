using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_popup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the rectangle for the popup annotation (llx, lly, urx, ury)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the popup annotation on the selected page
            PopupAnnotation popup = new PopupAnnotation(page, rect)
            {
                // Text displayed inside the popup window
                Contents = "This is a popup annotation added via Aspose.Pdf.",
                // Open the popup automatically when the PDF is opened
                Open = true,
                // Optional: set a background color for the popup
                Color = Aspose.Pdf.Color.LightYellow
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation added and saved to '{outputPath}'.");
    }
}