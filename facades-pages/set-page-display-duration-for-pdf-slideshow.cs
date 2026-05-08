using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "slideshow.pdf"; // PDF with page timings
        const int    seconds   = 5;               // display duration per page

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the PdfPageEditor facade and bind the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);                 // bind the document to the editor
                editor.DisplayDuration = seconds;    // set duration (in seconds) for each page
                editor.ApplyChanges();               // apply the changes to the document
                editor.Save(outputPdf);              // save the modified PDF
            }
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPdf}' with {seconds}s per page.");
    }
}