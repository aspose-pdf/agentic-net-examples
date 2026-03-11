using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputMhtPath  = "input.mht";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputMhtPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputMhtPath}");
            return;
        }

        // Load the MHT file into a PDF Document using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document pdfDoc = new Document(inputMhtPath, loadOptions))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor pageEditor = new PdfPageEditor(pdfDoc))
            {
                // Set a transition effect (e.g., vertical blinds) for all pages
                pageEditor.TransitionType = PdfPageEditor.BLINDV; // constant defined in PdfPageEditor
                pageEditor.TransitionDuration = 2;               // duration in seconds

                // Apply the changes to the document pages
                pageEditor.ApplyChanges();
            }

            // Save the modified PDF document
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"MHT converted to PDF with page transition saved as '{outputPdfPath}'.");
    }
}