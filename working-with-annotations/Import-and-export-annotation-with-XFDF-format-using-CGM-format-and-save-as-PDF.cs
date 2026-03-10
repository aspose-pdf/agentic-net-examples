using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input CGM file, intermediate XFDF file, and final PDF output
        const string cgmPath = "input.cgm";
        const string xfdfPath = "annotations.xfdf";
        const string pdfPath = "output.pdf";

        // Verify the CGM source exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // Load the CGM file into a PDF document using CgmLoadOptions
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Ensure the document has at least one page (CGM import creates pages)
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("No pages were created from the CGM file.");
                return;
            }

            // Add a sample text annotation to demonstrate export/import of annotations
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(firstPage, rect)
            {
                Title = "Sample Note",
                Contents = "This annotation was added before XFDF export.",
                Color = Aspose.Pdf.Color.Yellow,
                Open = true,
                Icon = TextIcon.Note
            };
            firstPage.Annotations.Add(txtAnn);

            // Export all annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");

            // (Optional) Remove existing annotations to prove import works
            // firstPage.Annotations.Delete(1); // Example of deleting the first annotation

            // Import annotations back from the XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine($"Annotations imported from XFDF: {xfdfPath}");

            // Save the final document as PDF
            doc.Save(pdfPath);
            Console.WriteLine($"PDF saved to: {pdfPath}");
        }
    }
}