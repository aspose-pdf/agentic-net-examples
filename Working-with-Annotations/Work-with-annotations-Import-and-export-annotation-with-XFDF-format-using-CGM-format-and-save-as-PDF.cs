using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string cgmPath   = "input.cgm";      // CGM source (input‑only format)
        const string pdfPath   = "output.pdf";     // Resulting PDF file
        const string xfdfPath  = "annotations.xfdf"; // XFDF file for annotations

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // Load the CGM file – CGM can only be loaded, not saved.
        CgmLoadOptions loadOptions = new CgmLoadOptions();
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // ------------------------------------------------------------
            // Add a sample annotation so that we have something to export.
            // ------------------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Sample",
                Contents = "This is a sample annotation.",
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(txtAnn);

            // Export all annotations to an XFDF file.
            doc.ExportAnnotationsToXfdf(xfdfPath);

            // Clear existing annotations to demonstrate the import step.
            page.Annotations.Clear();

            // Import annotations back from the XFDF file.
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the final PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}'.");
        Console.WriteLine($"XFDF saved to '{xfdfPath}'.");
    }
}