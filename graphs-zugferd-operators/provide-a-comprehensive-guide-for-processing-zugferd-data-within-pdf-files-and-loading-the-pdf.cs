using System;
using System.IO;
using Aspose.Pdf;

class ZugferdProcessor
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "invoice.pdf";          // PDF that may contain ZUGFeRD data
        const string outputPdfPath  = "invoice_updated.pdf"; // PDF after processing
        const string newZugferdXml  = "new_zugferd.xml";      // ZUGFeRD XML you want to embed
        const string extractedXml   = "extracted_zugferd.xml"; // Where to write extracted XML (if any)

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document – Document implements IDisposable, so wrap in using
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 1. Attempt to read embedded ZUGFeRD XML (if present)
            // ------------------------------------------------------------
            // ZUGFeRD data is stored as an embedded file referenced from the
            // document catalog entry "AF" (Associated Files).  GetCatalogValue
            // returns an object, so we need to convert it to a string safely.
            object afEntryObj = pdfDoc.GetCatalogValue("AF");
            string afEntry = afEntryObj != null ? afEntryObj.ToString() : null;

            if (!string.IsNullOrEmpty(afEntry))
            {
                Console.WriteLine("ZUGFeRD associated file detected in the PDF catalog.");
                // Placeholder: extraction logic would go here.
                // Example (pseudo‑code):
                //   var embeddedFile = pdfDoc.GetObjectById(objectId);
                //   using (var stream = embeddedFile.Stream) { ... }
                // If you implement extraction, you could write the XML to 'extractedXml'.
            }
            else
            {
                Console.WriteLine("No ZUGFeRD associated file found in the PDF.");
            }

            // ------------------------------------------------------------
            // 2. Embed (or replace) ZUGFeRD XML into the PDF
            // ------------------------------------------------------------
            if (File.Exists(newZugferdXml))
            {
                // BindXml(string) binds the XML file to the document.
                pdfDoc.BindXml(newZugferdXml);
                Console.WriteLine($"Embedded ZUGFeRD XML from '{newZugferdXml}'.");
            }
            else
            {
                Console.WriteLine($"ZUGFeRD XML to embed not found: {newZugferdXml}");
            }

            // ------------------------------------------------------------
            // 3. Save the updated PDF
            // ------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Updated PDF saved to '{outputPdfPath}'.");
        }

        // ------------------------------------------------------------
        // Optional: Export annotations to XFDF (if needed for further processing)
        // ------------------------------------------------------------
        // using (Document doc = new Document(inputPdfPath))
        // {
        //     doc.ExportAnnotationsToXfdf("annotations.xfdf");
        // }

        Console.WriteLine("Processing completed.");
    }
}
