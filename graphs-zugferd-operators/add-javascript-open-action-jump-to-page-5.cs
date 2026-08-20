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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that jumps to page 5 when the document opens
            JavascriptAction openJs = new JavascriptAction("this.pageNum = 5;");

            // Set the JavaScript as the document's open action
            doc.OpenAction = openJs;

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with open action to '{outputPath}'.");
    }
}