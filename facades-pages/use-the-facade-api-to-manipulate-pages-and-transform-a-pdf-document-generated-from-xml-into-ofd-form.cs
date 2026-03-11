using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Create a PDF document from the XML source
        using (Document doc = new Document())
        {
            // BindXml generates PDF content based on the XML (and optional XSLT)
            doc.BindXml(xmlPath);

            // Add a blank page at the end of the document
            doc.Pages.Add();

            // Manipulate pages using the PdfPageEditor facade
            PdfPageEditor pageEditor = new PdfPageEditor();
            pageEditor.BindPdf(doc);

            // Rotate the first page 90 degrees clockwise
            pageEditor.PageRotations[1] = 90;

            // Apply the changes made via the facade
            pageEditor.ApplyChanges();

            // Save the resulting PDF
            doc.Save(pdfPath);
            pageEditor.Close();

            Console.WriteLine($"PDF generated and saved to '{pdfPath}'.");
            Console.WriteLine("Note: Aspose.Pdf does not support saving to OFD format (OFD is input‑only).");
        }
    }
}