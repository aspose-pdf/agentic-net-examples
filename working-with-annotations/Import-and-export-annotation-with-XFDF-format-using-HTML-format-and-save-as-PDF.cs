using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input HTML file, intermediate XFDF file, and final PDF output
        const string htmlPath   = "input.html";
        const string xfdfPath   = "annotations.xfdf";
        const string outputPdf  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load HTML and convert it to a PDF document
            HtmlLoadOptions htmlLoad = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, htmlLoad))
            {
                // -------------------------------------------------
                // Add a sample annotation so that we have something to export
                // -------------------------------------------------
                Page firstPage = doc.Pages[1]; // 1‑based indexing
                // Fully qualified rectangle to avoid ambiguity with System.Drawing
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation txtAnn = new TextAnnotation(firstPage, rect)
                {
                    Title    = "Sample Note",
                    Contents = "This is a test annotation.",
                    Color    = Aspose.Pdf.Color.Yellow,
                    Open     = true,
                    Icon     = TextIcon.Note
                };
                firstPage.Annotations.Add(txtAnn);

                // -------------------------------------------------
                // Export all annotations to XFDF file
                // -------------------------------------------------
                doc.ExportAnnotationsToXfdf(xfdfPath);
                Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");

                // -------------------------------------------------
                // (Optional) Remove existing annotations to demonstrate import
                // -------------------------------------------------
                firstPage.Annotations.Delete(1); // delete the annotation we just added

                // -------------------------------------------------
                // Import annotations back from the XFDF file
                // -------------------------------------------------
                doc.ImportAnnotationsFromXfdf(xfdfPath);
                Console.WriteLine("Annotations imported from XFDF.");

                // -------------------------------------------------
                // Save the final document as PDF
                // -------------------------------------------------
                doc.Save(outputPdf);
                Console.WriteLine($"PDF saved to: {outputPdf}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
