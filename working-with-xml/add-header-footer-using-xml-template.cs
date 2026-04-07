using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string xmlTemplatePath = "header_footer.xml"; // XML that defines header/footer layout
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlTemplatePath))
        {
            Console.Error.WriteLine($"XML template not found: {xmlTemplatePath}");
            return;
        }

        // Load the source PDF document
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Load the XML template as a PDF document.
            // XmlLoadOptions converts the XML into a PDF representation.
            XmlLoadOptions xmlOpts = new XmlLoadOptions();
            using (Document tmplDoc = new Document(xmlTemplatePath, xmlOpts))
            {
                // Assume the first page of the template contains the header/footer design.
                // Create a stamp from that page.
                PdfPageStamp stamp = new PdfPageStamp(tmplDoc.Pages[1]);

                // Apply the stamp to every page of the source document.
                foreach (Page page in srcDoc.Pages)
                {
                    page.AddStamp(stamp);
                }

                // Save the result.
                srcDoc.Save(outputPdfPath);
                Console.WriteLine($"PDF with header/footer saved to '{outputPdfPath}'.");
            }
        }
    }
}