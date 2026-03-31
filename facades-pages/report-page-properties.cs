using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document document = new Document(inputPath))
        {
            // Initialize the page editor and bind the document
            PdfPageEditor pageEditor = new PdfPageEditor();
            pageEditor.BindPdf(document);

            // Report original page properties
            Console.WriteLine("=== Original Page Properties ===");
            ReportPageProperties(pageEditor);

            // Apply edits: rotate 90°, zoom to 150%, and set page size to A4 (595 x 842 points)
            pageEditor.Rotation = 90;
            pageEditor.Zoom = 1.5f;
            pageEditor.PageSize = new PageSize(595, 842);
            pageEditor.ApplyChanges();

            // Report edited page properties
            Console.WriteLine("=== Edited Page Properties ===");
            ReportPageProperties(pageEditor);

            // Save the edited PDF
            document.Save(outputPath);
            Console.WriteLine($"Edited PDF saved to '{outputPath}'.");

            // Clean up the editor
            pageEditor.Close();
        }
    }

    static void ReportPageProperties(PdfPageEditor editor)
    {
        int pageCount = editor.GetPages();
        for (int i = 1; i <= pageCount; i++)
        {
            PageSize pageSize = editor.GetPageSize(i);
            int rotation = editor.GetPageRotation(i);
            float zoom = editor.Zoom; // Zoom is applied uniformly to all pages
            Console.WriteLine($"Page {i}: Width={pageSize.Width}, Height={pageSize.Height}, Rotation={rotation}, Zoom={zoom}");
        }
    }
}
