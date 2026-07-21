using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Required for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set a JavaScript open action, and save.
        using (Document doc = new Document(inputPath))
        {
            // JavaScript to navigate to page 10 (PDF JavaScript uses zero‑based page numbers).
            doc.OpenAction = new JavascriptAction("this.pageNum = 9;");

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript open action: '{outputPath}'");
    }
}
