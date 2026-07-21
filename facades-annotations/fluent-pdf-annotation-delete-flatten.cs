using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class FluentPdfAnnotationEditor : IDisposable
{
    private readonly PdfAnnotationEditor _editor = new PdfAnnotationEditor();

    public FluentPdfAnnotationEditor BindPdf(string path)
    {
        _editor.BindPdf(path);
        return this;
    }

    public FluentPdfAnnotationEditor DeleteAnnotations()
    {
        _editor.DeleteAnnotations();
        return this;
    }

    public FluentPdfAnnotationEditor FlattenAnnotations()
    {
        _editor.FlatteningAnnotations();
        return this;
    }

    public FluentPdfAnnotationEditor Save(string path)
    {
        _editor.Save(path);
        return this;
    }

    public void Dispose()
    {
        _editor?.Dispose();
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // -------------------------------------------------------------------
        // 1️⃣ Create a minimal PDF with at least one annotation so the demo has
        //    something to delete/flatten. This satisfies the sandbox requirement
        //    that no external file is pre‑existing.
        // -------------------------------------------------------------------
        using (Document seed = new Document())
        {
            Page page = seed.Pages.Add();
            // Add a simple text annotation.
            var txtAnn = new TextAnnotation(page, new Rectangle(100, 600, 200, 650))
            {
                Title = "Demo",
                Contents = "Sample annotation"
            };
            page.Annotations.Add(txtAnn);
            seed.Save(inputPath);
        }

        // -------------------------------------------------------------------
        // 2️⃣ Fluent workflow: bind → delete → flatten → save, all in one chain.
        // -------------------------------------------------------------------
        using (var editor = new FluentPdfAnnotationEditor())
        {
            editor.BindPdf(inputPath)
                  .DeleteAnnotations()
                  .FlattenAnnotations()
                  .Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
