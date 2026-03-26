using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

public class PdfAnnotationHelper : IDisposable
{
    private readonly PdfAnnotationEditor _editor;
    private bool _isBound = false;

    public PdfAnnotationHelper()
    {
        _editor = new PdfAnnotationEditor();
    }

    /// <summary>
    /// Binds the helper to an existing PDF file. If the file does not exist, a simple one‑page PDF is created automatically.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file.</param>
    public void Bind(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path cannot be null or empty.", nameof(pdfPath));

        // If the file is missing, create a minimal PDF so the rest of the workflow can continue.
        if (!File.Exists(pdfPath))
        {
            var doc = new Document();
            doc.Pages.Add();
            doc.Save(pdfPath);
        }

        _editor.BindPdf(pdfPath);
        _isBound = true;
    }

    public void DeleteAll()
    {
        EnsureBound();
        _editor.DeleteAnnotations();
    }

    public void DeleteByName(string annotationName)
    {
        EnsureBound();
        if (string.IsNullOrEmpty(annotationName))
            throw new ArgumentException("Annotation name cannot be null or empty.", nameof(annotationName));
        _editor.DeleteAnnotation(annotationName);
    }

    public void DeleteByTypes(AnnotationType[] types)
    {
        EnsureBound();
        if (types == null) throw new ArgumentNullException(nameof(types));
        foreach (AnnotationType type in types)
        {
            // PdfAnnotationEditor expects the annotation subtype as a string.
            _editor.DeleteAnnotations(type.ToString());
        }
    }

    public void FlattenAll()
    {
        EnsureBound();
        _editor.FlatteningAnnotations();
    }

    public void FlattenRange(int startPage, int endPage, AnnotationType[] types)
    {
        EnsureBound();
        if (startPage < 1 || endPage < startPage)
            throw new ArgumentException("Invalid page range supplied.");
        _editor.FlatteningAnnotations(startPage, endPage, types);
    }

    public void Save(string outputPath)
    {
        EnsureBound();
        if (string.IsNullOrWhiteSpace(outputPath))
            throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPath));
        _editor.Save(outputPath);
    }

    public void Close()
    {
        if (_isBound)
        {
            _editor.Close();
            _isBound = false;
        }
    }

    private void EnsureBound()
    {
        if (!_isBound)
            throw new InvalidOperationException("PdfAnnotationHelper is not bound to a PDF. Call Bind() first.");
    }

    public void Dispose()
    {
        Close();
        _editor?.Dispose();
    }
}

public class Program
{
    public static void Main()
    {
        string inputPdf = "sample.pdf";
        string outputPdf = "sample_clean.pdf";

        // Use a using‑statement to guarantee proper disposal of resources.
        using (var helper = new PdfAnnotationHelper())
        {
            helper.Bind(inputPdf);
            helper.DeleteAll();
            helper.FlattenAll();
            helper.Save(outputPdf);
        }

        Console.WriteLine($"Annotations processed and saved to '{outputPdf}'.");
    }
}
