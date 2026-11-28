using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfContentEditor
using Aspose.Pdf.Annotations; // JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_rotation_actions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF with the high‑level Document API.
        Document doc = new Document(inputPath);

        // JavaScript that will be executed when a page is opened.
        // Aspose.Pdf does not expose a dedicated "Rotate" page event,
        // so we attach the script to the page's Open action.
        string js = "app.alert('Page has been rotated');";

        // Attach the script to every page via the page's Actions collection.
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Page page = doc.Pages[i];
            page.Actions.OnOpen = new JavascriptAction(js);
        }

        // Save the modified PDF using PdfContentEditor – this preserves the actions added above.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(doc);
        editor.Save(outputPath);

        Console.WriteLine($"PDF saved with page‑open actions: {outputPath}");
    }
}
