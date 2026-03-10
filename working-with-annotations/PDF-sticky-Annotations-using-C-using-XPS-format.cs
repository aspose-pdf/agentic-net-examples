using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output XPS file path
        const string outputXps = "output.xps";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Create a sticky note (TextAnnotation) on the first page
                // Rectangle coordinates are in points (bottom‑left origin)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);
                TextAnnotation sticky = new TextAnnotation(pdfDoc.Pages[1], rect)
                {
                    // Title appears in the annotation’s popup window title bar
                    Title = "Reviewer",
                    // Contents is the text shown inside the sticky note
                    Contents = "Please review this section.",
                    // Set a background color for the note
                    Color = Aspose.Pdf.Color.Yellow,
                    // Open the note by default when the document is opened
                    Open = true,
                    // Icon can be set to any of the predefined TextIcon values
                    Icon = TextIcon.Note
                };

                // Add the annotation to the page’s annotation collection
                pdfDoc.Pages[1].Annotations.Add(sticky);

                // Prepare XPS save options (no special settings required)
                XpsSaveOptions xpsOptions = new XpsSaveOptions();

                // Save the modified document as XPS
                pdfDoc.Save(outputXps, xpsOptions);
            }

            Console.WriteLine($"Sticky annotation added and PDF saved as XPS: '{outputXps}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}