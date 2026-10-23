using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a JavaScript action that navigates to page 10 when the document is opened.
            // PDF JavaScript uses zero‑based page numbers, so page 10 is index 9.
            JavascriptAction jsAction = new JavascriptAction("this.pageNum = 9;");

            // Assign the action as the document's open action.
            doc.OpenAction = jsAction;

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript open action to '{outputPath}'.");
    }
}