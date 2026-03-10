using System;
using System.IO;
using Aspose.Pdf;

class ZugFerdHandler
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";
        const string xmlExtractPath = "extracted_zugferd.xml";
        const string xmlToBindPath = "updated_zugferd.xml";
        const string outputPdfPath = "invoice_with_zugferd.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document (using ensures deterministic disposal)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // ------------------------------------------------------------
            // 1. Extract embedded ZUGFeRD XML (if present) and save to file
            // ------------------------------------------------------------
            // GetCatalogValue returns an object; cast it to string safely.
            string zugFerdXml = pdfDoc.GetCatalogValue("ZUGFeRD") as string;
            if (!string.IsNullOrEmpty(zugFerdXml))
            {
                File.WriteAllText(xmlExtractPath, zugFerdXml);
                Console.WriteLine($"ZUGFeRD XML extracted to: {xmlExtractPath}");
            }
            else
            {
                Console.WriteLine("No ZUGFeRD XML found in the PDF.");
            }

            // ------------------------------------------------------------
            // 2. Bind (embed) ZUGFeRD XML back into the PDF to enable persistence
            // ------------------------------------------------------------
            if (File.Exists(xmlToBindPath))
            {
                pdfDoc.BindXml(xmlToBindPath);
                Console.WriteLine($"ZUGFeRD XML bound from: {xmlToBindPath}");
            }
            else
            {
                Console.WriteLine($"XML to bind not found: {xmlToBindPath}");
            }

            // ------------------------------------------------------------
            // 3. Save the modified PDF (PDF format, no special SaveOptions needed)
            // ------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Modified PDF saved to: {outputPdfPath}");
        }
    }
}
