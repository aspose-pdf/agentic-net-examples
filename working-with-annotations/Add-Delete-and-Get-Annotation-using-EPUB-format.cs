using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputEpubPath = "output.epub";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // -------------------------------------------------
                // Add a text annotation to the first page
                // -------------------------------------------------
                Aspose.Pdf.Page page = doc.Pages[1]; // 1‑based indexing
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                Aspose.Pdf.Annotations.TextAnnotation textAnn = new Aspose.Pdf.Annotations.TextAnnotation(page, rect)
                {
                    Contents = "Sample annotation added programmatically",
                    Title    = "MyTitle",
                    Name     = "MyAnnot",                     // identifier for later deletion
                    Color    = Aspose.Pdf.Color.Yellow
                };
                page.Annotations.Add(textAnn);

                // -------------------------------------------------
                // Delete the annotation by its name using PdfAnnotationEditor
                // -------------------------------------------------
                using (Aspose.Pdf.Facades.PdfAnnotationEditor editor = new Aspose.Pdf.Facades.PdfAnnotationEditor())
                {
                    editor.BindPdf(doc);               // bind the same document instance
                    editor.DeleteAnnotation("MyAnnot"); // removes the annotation we just added
                }

                // -------------------------------------------------
                // Retrieve (list) remaining annotations on the first page
                // -------------------------------------------------
                Console.WriteLine("Remaining annotations on page 1:");
                foreach (Aspose.Pdf.Annotations.Annotation ann in page.Annotations)
                {
                    // Title is available only on markup annotations; cast safely
                    string title = ann is Aspose.Pdf.Annotations.MarkupAnnotation markup ? markup.Title : "N/A";
                    Console.WriteLine($"- Name: {ann.Name}, Title: {title}, Contents: {ann.Contents}");
                }

                // -------------------------------------------------
                // Save the modified document as EPUB (explicit save options required)
                // -------------------------------------------------
                Aspose.Pdf.EpubSaveOptions epubOptions = new Aspose.Pdf.EpubSaveOptions
                {
                    ContentRecognitionMode = Aspose.Pdf.EpubSaveOptions.RecognitionMode.Flow
                };

                try
                {
                    doc.Save(outputEpubPath, epubOptions);
                    Console.WriteLine($"Document saved as EPUB: {outputEpubPath}");
                }
                catch (TypeInitializationException)
                {
                    // EPUB conversion may rely on GDI+; handle non‑Windows platforms gracefully
                    Console.WriteLine("EPUB conversion requires Windows GDI+. Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}