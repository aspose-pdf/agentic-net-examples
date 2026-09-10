using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

public static class AnnotationReport
{
    // Returns a dictionary where key = page number (1‑based) and value = annotation count on that page
    public static Dictionary<int, int> GetAnnotationsCountPerPage(string pdfPath)
    {
        var counts = new Dictionary<int, int>();
        // Use a using‑statement to guarantee disposal of the facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfPath);
            Document doc = editor.Document;
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                counts[i] = page.Annotations.Count;
            }
            editor.Close();
        }
        return counts;
    }
}

class Program
{
    static void Main()
    {
        const string samplePath = "sample.pdf";

        // ------------------------------------------------------------
        // Create a self‑contained sample PDF with a few pages/annotations
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Page 1 – two text annotations
            Page page1 = doc.Pages.Add();
            var rect1 = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            var ann1 = new TextAnnotation(page1, rect1) { Title = "Note1", Contents = "First note" };
            page1.Annotations.Add(ann1);

            var rect2 = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            var ann2 = new TextAnnotation(page1, rect2) { Title = "Note2", Contents = "Second note" };
            page1.Annotations.Add(ann2);

            // Page 2 – one text annotation
            Page page2 = doc.Pages.Add();
            var rect3 = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            var ann3 = new TextAnnotation(page2, rect3) { Title = "Note3", Contents = "Third note" };
            page2.Annotations.Add(ann3);

            // Persist the PDF so the reporting method can open it
            doc.Save(samplePath);
        }

        // ------------------------------------------------------------
        // Retrieve annotation counts per page
        // ------------------------------------------------------------
        Dictionary<int, int> pageAnnotationCounts = AnnotationReport.GetAnnotationsCountPerPage(samplePath);

        foreach (var kvp in pageAnnotationCounts)
        {
            Console.WriteLine($"Page {kvp.Key}: {kvp.Value} annotation(s)");
        }
    }
}
