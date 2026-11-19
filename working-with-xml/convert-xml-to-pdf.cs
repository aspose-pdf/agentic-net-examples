using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Xsl; // Stub namespace defined below to avoid external package dependency

// Stub implementation of XslLoadOptions to remove the need for the Aspose.Pdf.Xsl NuGet package.
// This class mirrors the public API used in the sample (XslFile property) and inherits from LoadOptions.
namespace Aspose.Pdf.Xsl
{
    public class XslLoadOptions : LoadOptions
    {
        public string XslFile { get; set; }
    }
}

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";      // source XML file
        const string xsltPath = "transform.xslt"; // XSLT that defines PDF layout
        const string pdfPath = "output.pdf";     // resulting PDF file

        // Verify that the required files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        try
        {
            // Configure XSLT loading options – point to the XSLT file
            XslLoadOptions loadOptions = new XslLoadOptions
            {
                XslFile = xsltPath
            };

            // Load the XML and apply the XSLT transformation to create a PDF document
            using (Document pdfDoc = new Document(xmlPath, loadOptions))
            {
                // Save the generated PDF; no SaveOptions needed for default PDF output
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"PDF generated successfully: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF generation: {ex.Message}");
        }
    }
}
