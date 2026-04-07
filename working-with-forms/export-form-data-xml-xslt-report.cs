using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";            // PDF with form fields
        const string outputXml  = "formData.xml";         // Exported form data
        const string xsltFile   = "reportTransform.xslt"; // XSLT to create the report
        const string reportFile = "customReport.html";    // Resulting report

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xsltFile))
        {
            Console.Error.WriteLine($"XSLT not found: {xsltFile}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Export the form data to an XML file using the Facade Form class.
            // The regular Document.Form class does not expose ExportXml; the
            // Facade version works with streams.
            // -----------------------------------------------------------------
            using (Form form = new Form())
            {
                form.BindPdf(inputPdf);
                using (FileStream fs = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXml(fs);
                }
            }
            Console.WriteLine($"Form data exported to '{outputXml}'.");

            // -----------------------------------------------------------------
            // Transform the exported XML using the provided XSLT.
            // XslCompiledTransform.Transform has an overload that accepts a
            // TextWriter (e.g., StreamWriter). Using that avoids the overload
            // that expects a Stream, which caused the original compile error.
            // -----------------------------------------------------------------
            XslCompiledTransform transformer = new XslCompiledTransform();
            transformer.Load(xsltFile);

            using (StreamWriter writer = new StreamWriter(reportFile))
            {
                transformer.Transform(outputXml, null, writer);
            }

            Console.WriteLine($"Custom report generated at '{reportFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
