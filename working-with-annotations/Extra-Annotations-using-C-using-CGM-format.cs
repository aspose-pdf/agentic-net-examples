using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // for GoToURIAction
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputCgmPath  = "input.cgm";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputCgmPath}");
            return;
        }

        try
        {
            // Load CGM file and convert it to a PDF document
            using (Document doc = new Document(inputCgmPath, new CgmLoadOptions()))
            {
                // Ensure there is at least one page to annotate
                if (doc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("No pages were created from the CGM file.");
                    return;
                }

                // Add a text annotation on the first page
                Page firstPage = doc.Pages[1];
                Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation textAnn = new TextAnnotation(firstPage, textRect)
                {
                    Title    = "Note",
                    Contents = "This is an extra text annotation added after CGM import.",
                    Color    = Aspose.Pdf.Color.Yellow,
                    Open     = true
                };
                firstPage.Annotations.Add(textAnn);

                // Add a link annotation that points to an external URL
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 400, 300, 450);
                LinkAnnotation linkAnn = new LinkAnnotation(firstPage, linkRect)
                {
                    Color = Aspose.Pdf.Color.Blue
                };
                // Use GoToURIAction for external links
                linkAnn.Action = new GoToURIAction("https://www.example.com");
                firstPage.Annotations.Add(linkAnn);

                // Save the resulting PDF
                doc.Save(outputPdfPath);
                Console.WriteLine($"PDF with extra annotations saved to '{outputPdfPath}'.");
            }
        }
        catch (PdfException ex)
        {
            // Handles errors specific to Aspose.Pdf processing
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Generic fallback for any other errors (e.g., invalid CGM format)
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}