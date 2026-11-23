using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchText = "Sample Text"; // text to locate

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
                // Search for the specified text and obtain its fragment(s)
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
                doc.Pages.Accept(absorber);

                // If no fragment is found, exit
                if (absorber.TextFragments.Count == 0)
                {
                    Console.WriteLine($"Text \"{searchText}\" not found.");
                    doc.Save(outputPath); // still save original if needed
                    return;
                }

                // Use the first occurrence (0‑based index)
                TextFragment fragment = absorber.TextFragments[0];
                // The TextFragment provides the page via the Page property
                Page page = fragment.Page;
                // Its bounding rectangle
                Aspose.Pdf.Rectangle fragRect = fragment.Rectangle;

                // Create a FreeTextAnnotation positioned over the found text
                // The constructor requires a DefaultAppearance (font, size, color)
                var defaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
                FreeTextAnnotation freeText = new FreeTextAnnotation(page, fragRect, defaultAppearance)
                {
                    Contents = "Annotation added by FreeTextAnnotation.",
                    Color = Aspose.Pdf.Color.Yellow // background colour of the annotation
                };

                // Border must be created with the annotation as its parent; it has no Color property
                freeText.Border = new Border(freeText) { Width = 1 };

                // Add the annotation to the page
                page.Annotations.Add(freeText);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with annotation to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
