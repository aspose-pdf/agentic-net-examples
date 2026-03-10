using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string xfdfPath      = "annotations.xfdf";
        const string resultPdfPath = "result.pdf";

        // ------------------------------------------------------------
        // 1. Create a simple PDF with one page and add a figure annotation
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle for the figure (square) annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // SquareAnnotation derives from CommonFigureAnnotation and represents a figure
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                Color   = Aspose.Pdf.Color.Blue,          // border color
                InteriorColor = Aspose.Pdf.Color.LightGray, // fill color
                Contents = "Sample square figure annotation",
                Title    = "Figure 1"
            };

            // Add the annotation to the page
            page.Annotations.Add(square);

            // Save the PDF that now contains the figure annotation
            doc.Save(sourcePdfPath);
        }

        // ------------------------------------------------------------
        // 2. Export the annotations from the PDF to an XFDF (XML) file
        // ------------------------------------------------------------
        using (Document doc = new Document(sourcePdfPath))
        {
            // Export all annotations to XFDF (XML) format
            doc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // ------------------------------------------------------------
        // 3. Create a new PDF (could be a copy of the original) and import
        //    the figure annotations from the XFDF file
        // ------------------------------------------------------------
        using (Document doc = new Document(sourcePdfPath))
        {
            // Import annotations from the XFDF file into the document
            // Two overloads exist – using a file path is sufficient here
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the resulting PDF with the imported figure annotations
            doc.Save(resultPdfPath);
        }

        Console.WriteLine($"Figure annotation added, exported to XFDF, and re‑imported successfully.");
        Console.WriteLine($"Source PDF : {Path.GetFullPath(sourcePdfPath)}");
        Console.WriteLine($"XFDF file  : {Path.GetFullPath(xfdfPath)}");
        Console.WriteLine($"Result PDF : {Path.GetFullPath(resultPdfPath)}");
    }
}