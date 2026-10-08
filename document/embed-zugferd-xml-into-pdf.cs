using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "invoice.pdf";      // existing PDF
        const string zugferdXmlPath = "invoice.xml";      // ZUGFeRD XML file
        const string outputPdfPath  = "invoice_with_zugferd.pdf";

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }

        // Load the PDF, embed the XML, and save
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the XML file (embedded file)
            // Use the constructor that accepts the file path and a description.
            FileSpecification xmlFileSpec = new FileSpecification(zugferdXmlPath, "ZUGFeRD invoice data");
            // Optional: set additional parameters such as modification date
            xmlFileSpec.Params.ModDate = DateTime.UtcNow;

            // Add the embedded file to the PDF
            pdfDoc.EmbeddedFiles.Add(xmlFileSpec);

            // OPTIONAL: Mark the PDF as PDF/A-3 (required for ZUGFeRD compliance)
            // pdfDoc.Convert("conversion_log.xml", PdfFormat.PDF_A_3, ConvertErrorAction.Delete);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        // Re-open the saved PDF to verify the embedded XML is present
        using (Document verifyDoc = new Document(outputPdfPath))
        {
            bool found = false;
            foreach (FileSpecification fileSpec in verifyDoc.EmbeddedFiles)
            {
                if (fileSpec.Name.Equals(Path.GetFileName(zugferdXmlPath), StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                    Console.WriteLine("ZUGFeRD XML successfully embedded.");
                    // Write the embedded XML back to disk for further inspection
                    using (FileStream outStream = File.Create("extracted_invoice.xml"))
                    using (Stream content = fileSpec.Contents)
                    {
                        content.CopyTo(outStream);
                    }
                    break;
                }
            }

            if (!found)
                Console.WriteLine("ZUGFeRD XML not found in the PDF.");
        }
    }
}
