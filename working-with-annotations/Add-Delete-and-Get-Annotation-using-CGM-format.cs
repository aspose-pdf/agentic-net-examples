using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputCgmPath  = "input.cgm";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"File not found: {inputCgmPath}");
            return;
        }

        // Load CGM file (input‑only format) into a PDF document
        using (Document doc = new Document(inputCgmPath, new CgmLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("No pages were created from the CGM file.");
                return;
            }

            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // -------------------------------------------------
            // Add a TextAnnotation to the page
            // -------------------------------------------------
            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title    = "Sample Note",
                Contents = "This is a text annotation added to a CGM‑derived PDF.",
                Color    = Aspose.Pdf.Color.Yellow,
                // Assign a unique name to retrieve later
                Name     = "MyTextAnnotation"
            };
            page.Annotations.Add(textAnn);

            // -------------------------------------------------
            // Get the annotation back by name
            // -------------------------------------------------
            // FindByName returns an Annotation; cast to TextAnnotation to access Title/Contents
            TextAnnotation found = page.Annotations.FindByName("MyTextAnnotation") as TextAnnotation;
            if (found != null)
            {
                Console.WriteLine($"Found annotation: Title='{found.Title}', Contents='{found.Contents}'");
            }
            else
            {
                Console.WriteLine("Annotation not found.");
            }

            // -------------------------------------------------
            // Delete the annotation (by object reference)
            // -------------------------------------------------
            page.Annotations.Delete(textAnn);

            // Verify deletion
            Annotation afterDelete = page.Annotations.FindByName("MyTextAnnotation");
            Console.WriteLine(afterDelete == null ? "Annotation successfully deleted." : "Deletion failed.");

            // -------------------------------------------------
            // Save the modified document as PDF
            // -------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPdfPath}'.");
    }
}
