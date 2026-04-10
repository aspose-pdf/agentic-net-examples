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

        // Load the PDF, set a JavaScript open action, and save.
        using (Document doc = new Document(inputPath))
        {
            // JavaScript to jump to page 5 when the document opens.
            // In PDF JavaScript, page numbers are zero‑based, so page 5 => index 4.
            string script = "this.pageNum = 4;";
            JavascriptAction openAction = new JavascriptAction(script);

            // Assign the action to the document's OpenAction property.
            doc.OpenAction = openAction;

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript open action to '{outputPath}'.");
    }
}