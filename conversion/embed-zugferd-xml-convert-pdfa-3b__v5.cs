using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath    = "Invoice.pdf";
        const string xmlPath    = "factur-x.xml";
        const string outputPath = "CleanedInvoice2.pdf";
        const string logPath    = "conversion_log.xml";

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
            // Load the source PDF
            using (Document doc = new Document(pdfPath))
            {
                // Embed the ZUGFeRD XML as an associated file (AF)
                using (FileStream xmlStream = File.OpenRead(xmlPath))
                {
                    // FileSpecification(stream, name, description) constructor
                    var fileSpec = new FileSpecification(xmlStream, Path.GetFileName(xmlPath), "ZUGFeRD invoice data")
                    {
                        MIMEType      = "text/xml",          // correct property name (capital "M")
                        AFRelationship = AFRelationship.Data   // marks the file as an associated data file
                    };

                    // Add the specification to the document's embedded files collection
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Convert to PDF/A‑3B and delete any non‑compliant elements during conversion
                doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the cleaned PDF/A‑3B document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Cleaned PDF/A‑3B saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
