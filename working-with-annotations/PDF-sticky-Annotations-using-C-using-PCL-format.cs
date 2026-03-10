using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPclPath  = "input.pcl";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"File not found: {inputPclPath}");
            return;
        }

        // Load the PCL file (PCL is input‑only). The document is opened inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPclPath, new PclLoadOptions()))
        {
            // Ensure the document has at least one page.
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Create a sticky note (TextAnnotation) on the first page.
            // Use a fully qualified Rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation sticky = new TextAnnotation(doc.Pages[1], rect)
            {
                Title    = "Note",
                Contents = "This is a sticky annotation added to a PCL‑derived PDF.",
                Open     = true,               // Show the note open by default.
                Icon     = TextIcon.Note        // Standard sticky‑note icon.
            };

            // Add the annotation to the page's annotation collection.
            doc.Pages[1].Annotations.Add(sticky);

            // Save the modified document as PDF. No SaveOptions are needed because the target format is PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Sticky annotation added and saved to '{outputPdfPath}'.");
    }
}