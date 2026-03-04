using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string epubPath = "input.epub";
        const string pdfPath  = "intermediate.pdf";
        const string djvuPath = "output.djvu";

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Load the EPUB file and convert it to a PDF document.
        // EpubLoadOptions sets the default A4 page size and 300 dpi.
        using (Document pdfDoc = new Document(epubPath, new EpubLoadOptions()))
        {
            // OPTIONAL: manipulate pages using a Facade class.
            // Here we use PdfPageEditor (a Facade) to ensure all pages have
            // a rotation of 0 degrees. Other page‑level operations (size,
            // zoom, etc.) can be performed similarly.
            PdfPageEditor pageEditor = new PdfPageEditor(pdfDoc);
            pageEditor.Rotation = 0;          // no rotation
            pageEditor.ApplyChanges();        // apply the changes to the document

            // Save the intermediate PDF file.
            pdfDoc.Save(pdfPath);
        }

        // Aspose.Pdf does NOT support saving directly to DJVU format.
        // The conversion to DJVU must be performed with an external library
        // that can read PDF and produce DJVU.
        Console.WriteLine($"EPUB successfully converted to PDF: {pdfPath}");
        Console.WriteLine("DJVU export is not supported by Aspose.Pdf. Use a third‑party tool to convert the PDF to DJVU if required.");
    }
}