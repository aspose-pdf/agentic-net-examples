using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF with a text annotation
        using (Document doc = new Document())
        {
            // Add a single page (evaluation mode allows up to 4 pages)
            Page page = doc.Pages.Add();

            // Create a text annotation on the page
            Rectangle rect = new Rectangle(100, 600, 200, 500);
            TextAnnotation textAnnotation = new TextAnnotation(page, rect);
            textAnnotation.Title = "Sample";
            textAnnotation.Contents = "This is a sample annotation.";
            page.Annotations.Add(textAnnotation);

            // Save the PDF file
            doc.Save("sample.pdf");
        }

        // Open the PDF and export its annotations to an FDF (XFDF) file
        using (Document doc = new Document("sample.pdf"))
        {
            // Export all annotations to an XFDF file (using .fdf extension for compatibility)
            doc.ExportAnnotationsToXfdf("annotations.fdf");
        }
    }
}