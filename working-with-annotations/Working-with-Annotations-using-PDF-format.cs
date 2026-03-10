using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AnnotationDemo
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string xfdfFile = "annotations.xfdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // 1. Add a TextAnnotation (sticky note) to the first page
            // -------------------------------------------------
            Page firstPage = doc.Pages[1]; // 1‑based indexing

            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the annotation
            TextAnnotation textAnn = new TextAnnotation(firstPage, rect)
            {
                Title    = "Review Note",
                Contents = "Please verify this section.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,               // Show the popup when the document is opened
                Icon     = TextIcon.Note        // Standard sticky‑note icon
            };

            // Add the annotation to the page's annotation collection
            firstPage.Annotations.Add(textAnn);

            // -------------------------------------------------
            // 2. Export all annotations to an XFDF file
            // -------------------------------------------------
            doc.ExportAnnotationsToXfdf(xfdfFile);
            Console.WriteLine($"Annotations exported to XFDF: {xfdfFile}");

            // -------------------------------------------------
            // 3. (Optional) Remove all annotations, then re‑import from XFDF
            // -------------------------------------------------
            // Clear existing annotations using a reverse‑for loop to avoid index‑out‑of‑range issues
            foreach (Page page in doc.Pages)
            {
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    page.Annotations.Delete(i);
                }
            }

            // Import annotations back from the XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfFile);
            Console.WriteLine("Annotations imported back from XFDF.");

            // -------------------------------------------------
            // 4. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved as: {outputPdf}");
        }
    }
}
