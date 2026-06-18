using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class PdfAnnotationHelper
{
    private PdfAnnotationEditor _editor;

    public PdfAnnotationHelper(string pdfPath)
    {
        _editor = new PdfAnnotationEditor();
        _editor.BindPdf(pdfPath);
    }

    public void DeleteAnnotation(string annotationName)
    {
        _editor.DeleteAnnotation(annotationName);
    }

    public void DeleteAllAnnotations()
    {
        _editor.DeleteAnnotations();
    }

    public void FlattenAllAnnotations()
    {
        _editor.FlatteningAnnotations();
    }

    public void Save(string outputPath)
    {
        _editor.Save(outputPath);
    }

    public void Close()
    {
        _editor.Close();
    }
}

public class Program
{
    public static void Main()
    {
        // Create a simple PDF file (self‑contained example)
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Use the wrapper to flatten annotations (none in this sample) and save the result
        PdfAnnotationHelper helper = new PdfAnnotationHelper("input.pdf");
        helper.FlattenAllAnnotations();
        helper.Save("output.pdf");
        helper.Close();
    }
}
