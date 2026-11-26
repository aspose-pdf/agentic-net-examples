using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "Invoice.pdf";
        const string xmlPath = "factur-x.xml";
        const string outputPdfPath = "NamingChecked.pdf";

        // Verify that the output file name follows the required convention
        if (!Path.GetFileName(outputPdfPath).Equals("NamingChecked.pdf", StringComparison.OrdinalIgnoreCase))
        {
            Console.Error.WriteLine("Output file name does not match the required naming convention.");
            return;
        }

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Factur‑X XML not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdfPath))
            {
                // Create a FileSpecification for the Factur‑X XML and embed it
                var xmlSpec = new FileSpecification("factur-x.xml", "ZUGFeRD Factur‑X invoice")
                {
                    MIMEType = "text/xml",
                    AFRelationship = AFRelationship.Alternative,
                    // Load the XML content into the specification
                    Contents = new MemoryStream(File.ReadAllBytes(xmlPath))
                };
                doc.EmbeddedFiles.Add(xmlSpec);

                // Convert the document to PDF/A‑3B (required for ZUGFeRD compliance)
                // The Convert method creates a conversion log; we can discard it or keep it.
                doc.Convert("conversion_log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the resulting PDF/A‑3B file
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF/A‑3B file saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
