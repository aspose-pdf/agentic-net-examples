using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input HTML file, intermediate XFDF file, and final PDF output
        const string htmlPath   = "input.html";
        const string xfdfPath   = "annotations.xfdf";
        const string pdfPath    = "output.pdf";

        // Verify the HTML source exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load HTML and convert to a PDF document (Windows only for GDI+)
            HtmlLoadOptions loadOpts = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOpts))
            {
                // Add a sample text annotation to demonstrate export/import
                // Fully qualified Rectangle avoids ambiguity with System.Drawing
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation txtAnn = new TextAnnotation(doc.Pages[1], rect)
                {
                    Title    = "Sample Note",
                    Contents = "This is a test annotation.",
                    Open     = true,
                    Icon     = TextIcon.Note,
                    Color    = Aspose.Pdf.Color.Yellow
                };
                doc.Pages[1].Annotations.Add(txtAnn);

                // Export all annotations to XFDF file
                doc.ExportAnnotationsToXfdf(xfdfPath);
                Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");

                // (Optional) Remove existing annotations to prove import works
                doc.Pages[1].Annotations.Delete(1); // 1‑based indexing

                // Import annotations back from the XFDF file
                doc.ImportAnnotationsFromXfdf(xfdfPath);
                Console.WriteLine("Annotations imported from XFDF.");

                // Save the final PDF document
                doc.Save(pdfPath);
                Console.WriteLine($"PDF saved to: {pdfPath}");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ and is Windows‑only
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}