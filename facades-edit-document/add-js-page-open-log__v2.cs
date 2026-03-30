using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                JavascriptAction jsAction = new JavascriptAction("app.console.println('Page ' + this.pageNum);");
                page.Actions.OnOpen = jsAction;
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with page‑open JavaScript to '{outputPath}'.");
    }
}