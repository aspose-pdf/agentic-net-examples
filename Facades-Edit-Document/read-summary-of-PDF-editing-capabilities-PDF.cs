using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a temporary PDF that will be used for all facade demonstrations
        string tempPdf = Path.Combine(Path.GetTempPath(), "sample.pdf");
        CreateSamplePdf(tempPdf);

        Console.WriteLine("=== Aspose.Pdf.Facades PDF Editing Capabilities Summary ===");

        // ------------------------------------------------------------
        // PdfContentEditor – edit the actual content of a PDF
        // ------------------------------------------------------------
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            // Bind the PDF file to the editor
            contentEditor.BindPdf(tempPdf);

            // The class provides operations such as:
            // ReplaceText, ReplaceImage, DeleteImage, DeleteStamp,
            // Create various annotations (links, file attachments, etc.)
            Console.WriteLine("- PdfContentEditor: edit content (ReplaceText, ReplaceImage, DeleteImage, DeleteStamp, Create annotations, etc.)");
        }

        // ------------------------------------------------------------
        // Form – work with AcroForm fields
        // ------------------------------------------------------------
        using (Form form = new Form())
        {
            form.BindPdf(tempPdf);

            // Capabilities include filling fields, retrieving values,
            // flattening fields, exporting/importing FDF, XFDF, JSON, XML, etc.
            Console.WriteLine("- Form: manipulate AcroForm fields (FillField, GetField, FlattenAllFields, Export/Import FDF, XFDF, JSON, XML, etc.)");
        }

        // ------------------------------------------------------------
        // PdfAnnotationEditor – manage annotations (comments)
        // ------------------------------------------------------------
        using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
        {
            annotationEditor.BindPdf(tempPdf);

            // Allows adding, deleting, extracting, modifying and flattening annotations.
            Console.WriteLine("- PdfAnnotationEditor: add, delete, modify, extract, flatten annotations.");
        }

        // ------------------------------------------------------------
        // PdfFileInfo – read/write document metadata
        // ------------------------------------------------------------
        using (PdfFileInfo fileInfo = new PdfFileInfo())
        {
            fileInfo.BindPdf(tempPdf);

            // Provides access to Author, Title, Keywords, page count,
            // encryption status, custom metadata, etc.
            Console.WriteLine("- PdfFileInfo: access and modify metadata (Author, Title, Keywords, page count, encryption status, etc.)");
        }

        // ------------------------------------------------------------
        // PdfPageEditor – edit page‑level properties
        // ------------------------------------------------------------
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(tempPdf);

            // Supports rotating pages, changing page size, setting zoom,
            // adding transition effects, moving page content, etc.
            Console.WriteLine("- PdfPageEditor: rotate pages, change page size, set zoom, add transitions, move content.");
        }

        // ------------------------------------------------------------
        // PdfViewer – lightweight rendering/printing without full Document load
        // ------------------------------------------------------------
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(tempPdf);

            // Can render pages to images, print the document, retrieve page count, etc.
            Console.WriteLine("- PdfViewer (Facades): render pages, print documents, get page count without loading full Document.");
        }

        // Clean up the temporary file
        try { File.Delete(tempPdf); } catch { }
    }

    // Helper method to create a minimal PDF used for the demonstrations
    static void CreateSamplePdf(string path)
    {
        // Document API is used inside a using block (proper disposal)
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Add a simple text fragment
            TextFragment tf = new TextFragment("Aspose.Pdf Facades sample");
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);

            // Save the PDF to the specified path
            doc.Save(path);
        }
    }
}