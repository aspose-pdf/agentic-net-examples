using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set a JavaScript open action to jump to page 5, and save.
        using (Document doc = new Document(inputPath))
        {
            // JavaScript: page numbers are zero‑based, so page 5 => index 4.
            string js = "this.pageNum = 4;";
            doc.OpenAction = new JavascriptAction(js);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with open‑action to page 5: {outputPath}");
    }
}