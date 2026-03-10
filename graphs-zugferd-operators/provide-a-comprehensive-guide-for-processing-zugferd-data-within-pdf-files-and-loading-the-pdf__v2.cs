using System;
using System.IO;
using Aspose.Pdf;

class ZugferdProcessor
{
    // Path to the input PDF that may contain ZUGFeRD data
    const string InputPdfPath = "invoice.pdf";

    // Path where extracted ZUGFeRD XML will be saved
    const string ExtractedXmlPath = "extracted_zugferd.xml";

    // Path to a ZUGFeRD XML file that you want to embed into a PDF
    const string NewZugferdXmlPath = "new_zugferd.xml";

    // Path for the output PDF with the new ZUGFeRD data embedded
    const string OutputPdfPath = "invoice_with_new_zugferd.pdf";

    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Load the PDF document (lifecycle: use using for deterministic disposal)
        // -----------------------------------------------------------------
        if (!File.Exists(InputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {InputPdfPath}");
            return;
        }

        using (Document pdfDoc = new Document(InputPdfPath))
        {
            // -----------------------------------------------------------------
            // 2. Attempt to retrieve embedded ZUGFeRD XML.
            //    ZUGFeRD data is stored in the PDF catalog under the key "ZUGFeRD".
            //    GetCatalogValue returns an object; cast it to string (or use ToString())
            //    before processing.
            // -----------------------------------------------------------------
            object catalogObj = pdfDoc.GetCatalogValue("ZUGFeRD");
            string zugferdCatalogValue = catalogObj as string ?? catalogObj?.ToString();

            if (!string.IsNullOrEmpty(zugferdCatalogValue))
            {
                // The catalog entry may contain the XML directly or a reference.
                // For simplicity, assume the XML is stored as a string.
                // Write the XML to a file for further processing.
                File.WriteAllText(ExtractedXmlPath, zugferdCatalogValue);
                Console.WriteLine($"ZUGFeRD XML extracted to: {ExtractedXmlPath}");
            }
            else
            {
                Console.WriteLine("No ZUGFeRD data found in the PDF catalog.");
            }

            // -----------------------------------------------------------------
            // 3. OPTIONAL: Embed a new ZUGFeRD XML file into the PDF.
            //    Use the BindXml method to associate the XML with the document.
            //    After binding, save the PDF to persist the change.
            // -----------------------------------------------------------------
            if (File.Exists(NewZugferdXmlPath))
            {
                // Bind the external XML file to the PDF.
                // This creates the necessary entries in the PDF catalog.
                pdfDoc.BindXml(NewZugferdXmlPath);

                // Save the modified PDF. Use the overload that takes a file path.
                // No SaveOptions are required because the output format is PDF.
                pdfDoc.Save(OutputPdfPath);
                Console.WriteLine($"New ZUGFeRD data embedded and PDF saved to: {OutputPdfPath}");
            }
            else
            {
                Console.WriteLine($"ZUGFeRD XML to embed not found: {NewZugferdXmlPath}");
            }
        }
    }
}
