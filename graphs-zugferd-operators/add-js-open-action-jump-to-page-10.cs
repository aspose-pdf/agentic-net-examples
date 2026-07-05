using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // JavascriptAction resides here

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // JavaScript to jump to page 10 when the document is opened.
            // In PDF JavaScript, page numbers are zero‑based, so page 10 is index 9.
            string js = "this.pageNum = 9;";

            // Assign the JavaScript action to the document's OpenAction property.
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript open action: '{outputPath}'");
    }
}