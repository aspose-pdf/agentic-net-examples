using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";
        const string xsltPath = "transform.xslt";
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        // Validate required files before proceeding
        if (!ValidateFile(xmlPath, "source XML")) return;
        if (!ValidateFile(xsltPath, "XSLT stylesheet")) return;
        if (!ValidateFile(pdfTemplatePath, "PDF template")) return;

        // Load the source XML document
        var xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Prepare the XSLT transformer
        var xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);

        // Transform XML and capture the result in a memory stream
        using (var transformedStream = new MemoryStream())
        {
            using (var writer = XmlWriter.Create(transformedStream, xslt.OutputSettings))
            {
                xslt.Transform(xmlDoc, writer);
            }
            // Reset the stream position so it can be read from the beginning
            transformedStream.Position = 0;

            // Load the transformed XML for field extraction
            var transformedDoc = new XmlDocument();
            transformedDoc.Load(transformedStream);

            // Open the PDF form template
            using (var pdfDoc = new Document(pdfTemplatePath))
            {
                // Expect transformed XML to contain <field name="FieldName">Value</field>
                var fieldNodes = transformedDoc.SelectNodes("//field");
                if (fieldNodes != null)
                {
                    foreach (XmlNode node in fieldNodes)
                    {
                        var fieldName = node.Attributes?["name"]?.Value;
                        var fieldValue = node.InnerText ?? string.Empty;

                        if (!string.IsNullOrEmpty(fieldName) && pdfDoc.Form != null)
                        {
                            // Retrieve the form field as a generic Field object
                            if (pdfDoc.Form[fieldName] is Field pdfField)
                            {
                                pdfField.Value = fieldValue;
                            }
                        }
                    }
                }

                // Save the populated PDF
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF saved to {outputPdfPath}");
    }

    /// <summary>
    /// Checks whether a file exists and writes a friendly message if it does not.
    /// </summary>
    /// <param name="path">The file path to check.</param>
    /// <param name="description">A short description used in the error message.</param>
    /// <returns>True if the file exists; otherwise false.</returns>
    private static bool ValidateFile(string path, string description)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine($"Error: {description} file not found at '{Path.GetFullPath(path)}'.");
            return false;
        }
        return true;
    }
}
