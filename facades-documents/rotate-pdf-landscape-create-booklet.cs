using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the intermediate rotated PDF, and the final booklet.
        const string inputPdf   = "input.pdf";
        const string rotatedPdf = "rotated.pdf";
        const string bookletPdf = "booklet.pdf";

        // Verify the source file exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Rotate all pages to landscape orientation.
        // ------------------------------------------------------------
        // PdfPageEditor works as a facade; it does NOT implement IDisposable,
        // so we do NOT wrap it in a using block.
        PdfPageEditor pageEditor = new PdfPageEditor();

        // Bind the original PDF file.
        pageEditor.BindPdf(inputPdf);

        // Rotate every page 90 degrees clockwise (portrait -> landscape).
        pageEditor.Rotation = 90; // Valid values: 0, 90, 180, 270

        // Apply the rotation changes.
        pageEditor.ApplyChanges();

        // Save the rotated PDF to a temporary file.
        pageEditor.Save(rotatedPdf);

        // ------------------------------------------------------------
        // Step 2: Create a booklet from the rotated PDF.
        // ------------------------------------------------------------
        // PdfFileEditor also does not implement IDisposable.
        PdfFileEditor fileEditor = new PdfFileEditor();

        // MakeBooklet returns true on success.
        bool bookletCreated = fileEditor.MakeBooklet(rotatedPdf, bookletPdf);

        if (bookletCreated)
        {
            Console.WriteLine($"Booklet successfully created: {bookletPdf}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }

        // Optional cleanup: delete the intermediate rotated PDF.
        try
        {
            if (File.Exists(rotatedPdf))
                File.Delete(rotatedPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }
    }
}