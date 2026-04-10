using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for any facade helpers if needed
using Aspose.Pdf.Tagged;   // for ITaggedContent (not required here but safe)

class Program
{
    static void Main()
    {
        // Paths to the source XML, the XSLT that defines pagination placeholders,
        // and the desired output PDF.
        const string xmlPath   = "source.xml";
        const string xslPath   = "pagination.xsl";
        const string outputPdf = "paged_output.pdf";

        // Verify that the input files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xslPath}");
            return;
        }

        try
        {
            // Load the XML using the XSLT.  XmlLoadOptions accepts a stream or file name
            // that contains the XSL transformation to be applied during loading.
            XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

            // Create the PDF document from the transformed XML.
            using (Document pdfDoc = new Document(xmlPath, loadOptions))
            {
                // The XSLT should have inserted at least one pagination artifact
                // (e.g., a placeholder like "#" in headers/footers).  UpdatePagination
                // walks all pages and replaces the placeholder with the actual page number.
                pdfDoc.Pages.UpdatePagination();

                // Save the resulting PDF.
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"PDF with automatic page numbers saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing document: {ex.Message}");
        }
    }
}