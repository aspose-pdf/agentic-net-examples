using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // optional, kept for completeness

class Program
{
    static void Main()
    {
        // Input XML file and optional XSL (none in this example)
        const string xmlPath = "input.xml";
        // Output PDF file
        const string pdfPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML with default options
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Use a using block for deterministic disposal of the Document
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Change PDF version to 1.7 using Document.Convert (Version property is read‑only)
            // The first argument is an optional log file path; we explicitly cast null to string to avoid overload ambiguity.
            pdfDoc.Convert((string)null, PdfFormat.v_1_7, ConvertErrorAction.Delete);

            // Save the generated PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with version 1.7: {pdfPath}");
    }
}
