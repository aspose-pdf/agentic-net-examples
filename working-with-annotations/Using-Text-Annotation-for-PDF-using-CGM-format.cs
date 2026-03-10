using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input CGM file (will be converted to PDF)
        const string cgmPath = "input.cgm";
        // Output PDF file with a text annotation added
        const string outputPdf = "output_with_annotation.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        try
        {
            // Load the CGM file using CgmLoadOptions (CGM is input‑only)
            using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
            {
                // Ensure the document has at least one page
                if (doc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The loaded document contains no pages.");
                    return;
                }

                // Choose the first page to place the annotation
                Page page = doc.Pages[1];

                // Define the rectangle for the annotation (left, bottom, right, top)
                // Use fully qualified type to avoid ambiguity with System.Drawing
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a TextAnnotation (sticky note) on the selected page
                TextAnnotation textAnn = new TextAnnotation(page, rect)
                {
                    Title    = "Note Title",                     // Title shown in the popup header
                    Contents = "This is a text annotation added after converting a CGM file.", // Annotation text
                    Color    = Aspose.Pdf.Color.Yellow,         // Color of the annotation icon
                    Open     = false                            // Initially closed (popup hidden)
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(textAnn);

                // Save the resulting PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF with text annotation saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}