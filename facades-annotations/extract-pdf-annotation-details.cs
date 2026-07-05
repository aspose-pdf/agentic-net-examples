using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class AnnotationInfo
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public Aspose.Pdf.Rectangle? Rect { get; set; }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Ensure a PDF exists so the example can run without external files.
        EnsureSamplePdf(pdfPath);

        // Initialize the annotation editor and bind the PDF file
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfPath);

            // Determine total number of pages using PdfFileInfo
            PdfFileInfo fileInfo = new PdfFileInfo(pdfPath);
            int pageCount = fileInfo.NumberOfPages;

            // Retrieve all possible annotation types
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

            // Extract annotations from all pages
            IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, allTypes);

            // Convert to custom objects for analysis
            List<AnnotationInfo> extracted = new List<AnnotationInfo>();
            foreach (Annotation ann in annotations)
            {
                extracted.Add(new AnnotationInfo
                {
                    Name = ann.Name,
                    Type = ann.AnnotationType.ToString(),
                    Rect = ann.Rect
                });
            }

            // Example output
            foreach (var info in extracted)
            {
                string name = info.Name ?? "<null>";
                string type = info.Type ?? "<null>";
                var rect = info.Rect;
                if (rect != null)
                {
                    Console.WriteLine($"Name: {name}, Type: {type}, Rect: [{rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY}]");
                }
                else
                {
                    Console.WriteLine($"Name: {name}, Type: {type}, Rect: <null>");
                }
            }
        }
    }

    /// <summary>
    /// Creates a minimal PDF with a single text annotation if the file does not already exist.
    /// This makes the sample self‑contained and eliminates the FileNotFoundException.
    /// </summary>
    private static void EnsureSamplePdf(string path)
    {
        if (File.Exists(path))
            return;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Add a simple text annotation so that extraction has something to return
            var rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650); // fully qualified to avoid ambiguity
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Name = "SampleAnnotation",
                Title = "Demo",
                Contents = "This is a sample annotation",
                Color = Aspose.Pdf.Color.Yellow // fully qualified to avoid ambiguity
            };
            page.Annotations.Add(txtAnn);

            // Save the PDF to the specified path
            doc.Save(path);
        }
    }
}
