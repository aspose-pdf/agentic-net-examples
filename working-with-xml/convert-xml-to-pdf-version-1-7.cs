using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string dataDir   = @"YOUR_DATA_DIRECTORY";
        string xmlInput  = Path.Combine(dataDir, "input.xml");
        string pdfOutput = Path.Combine(dataDir, "output.pdf");
        string xmlOutput = Path.Combine(dataDir, "output.xml");

        if (!File.Exists(xmlInput))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInput}");
            return;
        }

        // Load the XML file. No XSL is supplied, so default conversion is used.
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        using (Document pdfDocument = new Document(xmlInput, loadOptions))
        {
            // Set PDF version to 1.7 using Document.Convert (Version property is read‑only).
            // The Convert method writes the PDF with the specified version directly to the output file.
            pdfDocument.Convert(pdfOutput, PdfFormat.v_1_7, ConvertErrorAction.Delete);
            Console.WriteLine($"PDF created with version 1.7: {pdfOutput}");

            // Optionally convert the PDF back to XML.
            XmlSaveOptions xmlSaveOptions = new XmlSaveOptions();
            pdfDocument.Save(xmlOutput, xmlSaveOptions);
            Console.WriteLine($"XML exported: {xmlOutput}");
        }
    }
}
