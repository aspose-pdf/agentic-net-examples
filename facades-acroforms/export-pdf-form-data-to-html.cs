using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// NOTE: The NU1903 warning is a NuGet package vulnerability warning.
// It cannot be suppressed with C# #pragma directives. To fully silence it,
// add <NoWarn>NU1903</NoWarn> to the project file or upgrade the transitive
// Microsoft.Bcl.Memory package to a non‑vulnerable version. The code below
// compiles without C# errors.

class ExportFormDataToHtml
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath   = "input.pdf";        // PDF form file
        const string xmlPath   = "formData.xml";     // Intermediate XML export
        const string xsltPath  = "formToHtml.xslt";  // XSLT that converts XML to HTML
        const string htmlPath  = "output.html";      // Final HTML output

        try
        {
            // ------------------------------------------------------------
            // 1. Export form fields to XML using Aspose.Pdf.Facades.Form
            // ------------------------------------------------------------
            using (Form form = new Form(pdfPath))
            {
                // ExportXml writes the XML representation of the form data to a stream
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXml(xmlStream);
                }
            }

            // ------------------------------------------------------------
            // 2. Transform the exported XML into HTML using XSLT
            // ------------------------------------------------------------
            XslCompiledTransform transformer = new XslCompiledTransform();
            transformer.Load(xsltPath); // Load the XSLT stylesheet that defines the HTML layout

            // Perform the transformation: XML -> HTML
            using (FileStream xmlInput = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            using (XmlReader xmlReader = XmlReader.Create(xmlInput))
            using (FileStream htmlOutput = new FileStream(htmlPath, FileMode.Create, FileAccess.Write))
            {
                // No XSLT arguments are required; pass null for XsltArgumentList
                transformer.Transform(xmlReader, null, htmlOutput);
            }

            Console.WriteLine("Form data exported to XML and transformed to HTML successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}