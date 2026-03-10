using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source XML, intermediate PDF and final TIFF
        const string xmlPath = "input.xml";
        const string pdfPath = "intermediate.pdf";
        const string tiffPath = "output.tiff";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the XML and generate a PDF document.
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Bind the XML file to the Document. No XSL is required for simple conversion.
            doc.BindXml(xmlPath);

            // Save the generated PDF. Document.Save without SaveOptions writes PDF.
            doc.Save(pdfPath);
        }

        // ------------------------------------------------------------
        // 2. Manipulate PDF pages using PdfPageEditor (e.g., rotate all pages).
        // ------------------------------------------------------------
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the previously created PDF.
            editor.BindPdf(pdfPath);

            // Example manipulation: rotate every page by 90 degrees.
            editor.Rotation = 90;

            // Apply changes and overwrite the same PDF file.
            editor.Save(pdfPath);
        }

        // ------------------------------------------------------------
        // 3. Convert the manipulated PDF to a single multi‑page TIFF image.
        // ------------------------------------------------------------
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF to the converter.
            converter.BindPdf(pdfPath);

            // Prepare the converter (required before saving).
            converter.DoConvert();

            // Save all pages as one TIFF file using default settings.
            converter.SaveAsTIFF(tiffPath);
        }

        Console.WriteLine($"TIFF image created at '{tiffPath}'.");
    }
}