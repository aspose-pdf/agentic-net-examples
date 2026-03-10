using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string mhtPath = "input.mht";      // source MHT file
        const string pdfPath = "output.pdf";     // destination PDF file

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"File not found: {mhtPath}");
            return;
        }

        // Load the MHT file into a PDF Document using the provided MhtLoadOptions rule
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document doc = new Document(mhtPath, loadOptions))
        {
            // Create the PdfPageEditor facade and bind the loaded document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Apply a page transition effect to all pages
            // Example: vertical blinds transition (BLINDV) with a 2‑second duration
            editor.TransitionType = PdfPageEditor.BLINDV;
            editor.TransitionDuration = 2; // duration in seconds

            // Commit the changes to the document
            editor.ApplyChanges();

            // Save the edited PDF using the facade's Save method (lifecycle rule)
            editor.Save(pdfPath);
        }

        Console.WriteLine($"MHT converted to PDF with transition saved to '{pdfPath}'.");
    }
}