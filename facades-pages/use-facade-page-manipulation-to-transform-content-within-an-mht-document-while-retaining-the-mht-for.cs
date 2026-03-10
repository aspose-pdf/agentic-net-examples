using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input MHT file path
        const string inputMht = "input.mht";
        // Output file path – MHT cannot be saved directly, so we save as PDF
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputMht))
        {
            Console.Error.WriteLine($"File not found: {inputMht}");
            return;
        }

        // Load the MHT document using MhtLoadOptions (MHT is imported as PDF internally)
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document doc = new Document(inputMht, loadOptions))
        {
            // -----------------------------------------------------------------
            // Facade page manipulation: rotate all pages 90 degrees clockwise
            // -----------------------------------------------------------------
            PdfPageEditor pageEditor = new PdfPageEditor();
            pageEditor.BindPdf(doc);          // Bind the Document to the facade
            pageEditor.Rotation = 90;         // Set rotation for all pages
            pageEditor.ApplyChanges();        // Apply the changes to the document

            // -----------------------------------------------------------------
            // Save the transformed document.
            // Note: Aspose.Pdf does not support saving back to MHT; the result is saved as PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"MHT content transformed and saved as PDF: {outputPdf}");
    }
}