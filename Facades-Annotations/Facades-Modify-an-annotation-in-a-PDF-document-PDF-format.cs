using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document (load rule)
            Document pdfDocument = new Document(inputPath);

            // Ensure the document has at least one page
            if (pdfDocument.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The PDF does not contain any pages.");
                return;
            }

            // Access the first page (pages are 1‑based)
            Page page = pdfDocument.Pages[1];

            // Locate the first TextAnnotation on the page
            TextAnnotation? textAnnot = null;
            foreach (Annotation annot in page.Annotations)
            {
                if (annot is TextAnnotation ta)
                {
                    textAnnot = ta;
                    break;
                }
            }

            if (textAnnot == null)
            {
                Console.Error.WriteLine("No TextAnnotation found on the first page.");
                return;
            }

            // Modify annotation properties
            textAnnot.Contents = "Updated annotation text";
            textAnnot.Color = Color.Blue; // Change the annotation color

            // Initialize and assign a new border (border‑initialization rule)
            // Note: Border must be set after the annotation object is created.
            textAnnot.Border = new Border(textAnnot)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Save the modified PDF (document‑save rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Annotation modified and PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}