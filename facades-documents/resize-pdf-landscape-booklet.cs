using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF (portrait)
        const string tempPdf   = "temp_landscape.pdf"; // intermediate PDF after resizing
        const string outputPdf = "booklet_output.pdf"; // final booklet PDF

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Load the original PDF and change its page size to landscape.
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // PdfPageEditor works on a Document instance.
            PdfPageEditor pageEditor = new PdfPageEditor();
            pageEditor.BindPdf(doc);

            // Set the desired page size (A4). Rotation will turn the content to landscape.
            pageEditor.PageSize = PageSize.A4; // A4Landscape does not exist; use A4 + rotation.
            pageEditor.Rotation = 90;          // Rotate content 90° to achieve landscape layout.

            // Apply the changes to the document.
            pageEditor.ApplyChanges();

            // Save the modified PDF to a temporary file.
            doc.Save(tempPdf);
        }

        // -----------------------------------------------------------------
        // Step 2: Create a booklet from the resized (landscape) PDF.
        // -----------------------------------------------------------------
        // PdfFileEditor does NOT implement IDisposable, so no using block is needed.
        PdfFileEditor fileEditor = new PdfFileEditor();

        // MakeBooklet reads the input file and writes the booklet to the output file.
        bool success = fileEditor.MakeBooklet(tempPdf, outputPdf);

        if (success)
        {
            Console.WriteLine($"Booklet created successfully: {outputPdf}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }

        // Optional: clean up the intermediate file.
        try
        {
            if (File.Exists(tempPdf))
                File.Delete(tempPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }
    }
}
