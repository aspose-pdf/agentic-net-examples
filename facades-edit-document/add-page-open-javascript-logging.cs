using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (core API) inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Add a JavaScript action to each page that logs the page number when the page is opened.
            // this.pageNum is zero‑based, so we add 1 for a human‑readable number.
            string jsCode = "console.println('Page ' + (this.pageNum + 1));";

            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based page indexing
            {
                // Correct property is Page.Actions.OnOpen, not Page.PageAction.
                doc.Pages[i].Actions.OnOpen = new JavascriptAction(jsCode);
            }

            // Save the modified document to a temporary file.
            string tempFile = Path.GetTempFileName();
            doc.Save(tempFile);

            // Use a Facade class (PdfContentEditor) to bind the temporary file and produce the final output.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(tempFile);
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with page‑open JavaScript saved to '{outputPath}'.");
    }
}
