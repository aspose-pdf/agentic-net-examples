using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlInputPath   = "input.xml";   // XML data to bind
        const string pdfTempPath    = "temp.pdf";    // intermediate PDF
        const string pdfOutputPath  = "output.pdf"; // final PDF after page manipulation

        // Verify input XML exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Create a PDF document and bind the XML data to it.
            // ------------------------------------------------------------
            using (Document pdfDoc = new Document())
            {
                // BindXml loads the XML and creates PDF pages based on the XML layout.
                pdfDoc.BindXml(xmlInputPath);

                // Save the intermediate PDF (required before using PdfFileEditor).
                pdfDoc.Save(pdfTempPath);
            }

            // ------------------------------------------------------------
            // 2. Manipulate pages using the PdfFileEditor facade.
            //    Example: delete page 2 from the PDF.
            // ------------------------------------------------------------
            PdfFileEditor editor = new PdfFileEditor();

            // Delete page 2 (page numbers are 1‑based). The result is saved to pdfOutputPath.
            editor.Delete(pdfTempPath, new int[] { 2 }, pdfOutputPath);

            // Clean up the temporary file.
            if (File.Exists(pdfTempPath))
                File.Delete(pdfTempPath);

            Console.WriteLine($"PDF created and pages manipulated successfully: {pdfOutputPath}");

            // ------------------------------------------------------------
            // 3. NOTE: OFD format is not supported for export in Aspose.Pdf.
            //    The library can load OFD files (via OfdLoadOptions) but cannot
            //    save or convert a document to OFD. Therefore the final output
            //    remains a PDF.
            // ------------------------------------------------------------
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}