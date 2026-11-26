using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPath = "EmbedThenConvert.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // Embed ZUGFeRD XML as an associated file (AF Alternative)
                using (FileStream xmlStream = File.OpenRead(xmlPath))
                {
                    var fileSpec = new FileSpecification(xmlStream, Path.GetFileName(xmlPath), "ZUGFeRD invoice data")
                    {
                        MIMEType = "text/xml",
                        AFRelationship = AFRelationship.Alternative
                    };
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Convert the document to PDF/A-3B
                doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the PDF/A-3B compliant file
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A-3B file saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
