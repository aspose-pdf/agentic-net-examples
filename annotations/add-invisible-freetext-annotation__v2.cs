using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for DefaultAppearance

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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Choose the page to place the annotation (first page in this example)
                Page page = doc.Pages[1];

                // Define the rectangle where the FreeTextAnnotation will appear
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

                // Build the default appearance (font, size, colour)
                DefaultAppearance appearance = new DefaultAppearance("Arial", 14, System.Drawing.Color.Black);

                // Create the FreeTextAnnotation with the required appearance
                FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
                {
                    // Set the annotation text
                    Contents = "Sample free text annotation",

                    // Make the annotation invisible (it will not be displayed in viewers)
                    Flags = AnnotationFlags.Invisible
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(freeText);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with FreeTextAnnotation to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
