using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class PdfEditorWithLogging
{
    // Path to the log file
    private const string LogFilePath = "edit_log.txt";

    // Helper method to log an operation with timestamp and PDF file name
    private static void LogOperation(string pdfFileName, string operation)
    {
        string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{pdfFileName}\t{operation}";
        File.AppendAllLines(LogFilePath, new[] { entry });
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImg = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Ensure the log file exists
        if (!File.Exists(LogFilePath))
            File.WriteAllText(LogFilePath, "Timestamp\tPDF File\tOperation" + Environment.NewLine);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // 1. Add a text annotation to the first page
            // -------------------------------------------------
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnno = new TextAnnotation(firstPage, rect)
            {
                Title = "Note",
                Contents = "Added via Aspose.Pdf.Facades example.",
                Open = true,
                Icon = TextIcon.Note
            };
            firstPage.Annotations.Add(txtAnno);
            LogOperation(Path.GetFileName(outputPdf), "Added TextAnnotation");

            // -------------------------------------------------
            // 2. Add an image stamp to every page using the high‑level API
            // -------------------------------------------------
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                Background = false,
                Opacity = 0.5,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            int pageNumber = 1;
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
                LogOperation(Path.GetFileName(outputPdf), $"Added ImageStamp to page {pageNumber}");
                pageNumber++;
            }

            // -------------------------------------------------
            // 3. Save the final PDF
            // -------------------------------------------------
            doc.Save(outputPdf);
            LogOperation(Path.GetFileName(outputPdf), "Saved final PDF");
        }

        Console.WriteLine($"Editing completed. Log written to '{LogFilePath}'.");
    }
}
