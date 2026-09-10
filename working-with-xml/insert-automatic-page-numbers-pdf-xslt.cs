using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string xslPath = "pagination.xsl";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSL file not found: {xslPath}");
            return;
        }

        // Load XML and apply XSLT to generate a PDF document
        using (Document doc = new Document())
        {
            // Bind the XML source with the XSLT stylesheet
            doc.BindXml(xmlPath, xslPath);

            // Update pagination artifacts (page numbers) across all pages
            doc.Pages.UpdatePagination();

            // Save the resulting PDF with automatic page numbers
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with automatic page numbers saved to '{outputPdf}'.");
    }
}