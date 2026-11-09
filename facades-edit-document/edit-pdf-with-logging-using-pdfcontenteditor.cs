using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class PdfEditLogger
{
    // Path to the log file
    private const string LogFilePath = "pdf_edit_log.txt";

    // Append a log entry with timestamp, operation name and PDF file name
    private static void LogOperation(string operation, string pdfFilePath)
    {
        string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {operation} | {Path.GetFileName(pdfFilePath)}";
        File.AppendAllText(LogFilePath, entry + Environment.NewLine);
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImg  = "stamp.png";

        // Ensure required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Record loading of the source PDF
        LogOperation("Load", inputPdf);

        // Load the PDF using the high‑level Document API (Aspose.Pdf)
        Document doc = new Document(inputPdf);
        LogOperation("DocumentCreated", inputPdf);

        // ---------------------------------------------------------------------
        // 1. Add an image stamp to every page
        // ---------------------------------------------------------------------
        ImageStamp stamp = new ImageStamp(stampImg)
        {
            Background          = false,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Center
        };
        foreach (Page page in doc.Pages)
        {
            page.AddStamp(stamp);
        }
        LogOperation("AddStamp", inputPdf);

        // ---------------------------------------------------------------------
        // 2. Add a text annotation to the first page
        // ---------------------------------------------------------------------
        // Use the constructor that takes the target page and a rectangle to avoid
        // ambiguity and to work with all supported Aspose.Pdf versions.
        TextAnnotation txtAnno = new TextAnnotation(
            doc.Pages[1],
            new Rectangle(100, 500, 300, 550) // llx, lly, urx, ury
        )
        {
            Title    = "Note",
            Contents = "Sample text annotation",
            Open     = true,
            Icon     = TextIcon.Note
        };
        // Add the annotation to the page's Annotations collection
        doc.Pages[1].Annotations.Add(txtAnno);
        LogOperation("AddTextAnnotation", inputPdf);

        // ---------------------------------------------------------------------
        // 3. Save the edited PDF
        // ---------------------------------------------------------------------
        doc.Save(outputPdf);
        LogOperation("Save", outputPdf);

        Console.WriteLine($"Editing completed. Log written to '{LogFilePath}'.");
    }
}
