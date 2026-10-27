using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define the pages to be resized (all pages in this example)
        int[] pages = Enumerable.Range(1, doc.Pages.Count).ToArray();

        // Create resize parameters.
        // The Margins factory method creates a ContentsResizeParameters instance.
        // Here we use absolute margins of 20 points for left/right and 20 points for top/bottom.
        // If a mixed (percent/absolute) overload exists, replace the values accordingly.
        var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(20, 20, 20, 20);

        // Perform the resize operation on the specified pages
        PdfFileEditor editor = new PdfFileEditor();
        editor.ResizeContents(doc, pages, resizeParams);

        // Save the modified document
        doc.Save(outputPath);
        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}