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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that jumps to page 5 (pageNum is zero‑based, so 4)
            JavascriptAction openAction = new JavascriptAction("this.pageNum = 4;");

            // Assign the JavaScript action to be executed when the document opens
            doc.OpenAction = openAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with open‑action to page 5: {outputPath}");
    }
}