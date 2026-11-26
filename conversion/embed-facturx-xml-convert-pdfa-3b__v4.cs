using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPath = "OrderedInvoice.pdf";
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(pdfPath))
            {
                // Read the Factur‑X XML file into a memory stream
                byte[] xmlBytes = File.ReadAllBytes(xmlPath);
                using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
                {
                    // Create a file specification for the XML attachment
                    var fileSpec = new FileSpecification(xmlStream, "factur-x.xml", "ZUGFeRD Factur‑X XML")
                    {
                        MIMEType = "text/xml",               // MIME type for ZUGFeRD
                        AFRelationship = AFRelationship.Data // Relationship indicating embedded data
                    };

                    // Add the specification to the document's embedded files collection
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Convert the document to PDF/A‑3B (preserves original page order)
                using (FileStream logStream = new FileStream(conversionLog, FileMode.Create, FileAccess.Write))
                {
                    doc.Convert(logStream, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
                }

                // Save the resulting PDF/A‑3B file
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑3B file saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
