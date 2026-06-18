using System;
using System.IO;
using System.Drawing; // Added for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the Facade editor and bind the loaded document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Define the appearance: Helvetica, size 12, blue color (System.Drawing.Color)
                DefaultAppearance appearance = new DefaultAppearance(
                    "Helvetica",               // font name
                    12,                        // font size
                    System.Drawing.Color.Blue // text color (System.Drawing.Color)
                );

                // Define the annotation rectangle (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create the free‑text annotation on page 1 with the specified appearance
                FreeTextAnnotation freeText = new FreeTextAnnotation(doc.Pages[1], rect, appearance)
                {
                    Contents = "Sample free‑text annotation"
                };

                // Add the annotation to the page's annotation collection
                doc.Pages[1].Annotations.Add(freeText);

                // Save the modified PDF using the Facade
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
