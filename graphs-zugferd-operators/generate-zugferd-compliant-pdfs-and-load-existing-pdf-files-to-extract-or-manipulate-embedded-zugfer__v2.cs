using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TextFragment

class ZugferdHelper
{
    // Creates a simple PDF, embeds a ZUGFeRD XML file and saves the result.
    public static void CreateZugferdPdf(string outputPdfPath, string zugferdXmlPath)
    {
        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }

        // Create a new PDF document inside a using block (ensures disposal).
        using (Document pdfDoc = new Document())
        {
            // Add a single page with some placeholder text.
            Page page = pdfDoc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Invoice – ZUGFeRD compliant PDF"));

            // Bind the ZUGFeRD XML file to the PDF.
            // This embeds the XML as an associated file (AF entry) in the PDF catalog.
            pdfDoc.BindXml(zugferdXmlPath);

            // Save the PDF. No SaveOptions are required because the target format is PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"ZUGFeRD PDF created: {outputPdfPath}");
    }

    // Loads an existing PDF, extracts the embedded ZUGFeRD XML (if present) and writes it to a file.
    public static void ExtractZugferdXml(string inputPdfPath, string outputXmlPath)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // The embedded ZUGFeRD file is stored in the PDF catalog under the "AF" (Associated Files) entry.
            // GetCatalogValue returns an object; cast it to string (or handle the actual type accordingly).
            object afEntry = pdfDoc.GetCatalogValue("AF");
            string embeddedXml = afEntry as string;

            if (string.IsNullOrEmpty(embeddedXml))
            {
                Console.WriteLine("No ZUGFeRD XML found in the PDF.");
                return;
            }

            // Write the extracted XML to the specified output file.
            File.WriteAllText(outputXmlPath, embeddedXml);
        }

        Console.WriteLine($"Extracted ZUGFeRD XML saved to: {outputXmlPath}");
    }

    // Example usage.
    static void Main()
    {
        string pdfPath = "invoice_zugferd.pdf";
        string xmlPath = "invoice.xml";
        string extractedXmlPath = "extracted_invoice.xml";

        // Create a ZUGFeRD‑compliant PDF.
        CreateZugferdPdf(pdfPath, xmlPath);

        // Load the PDF and extract the embedded ZUGFeRD XML.
        ExtractZugferdXml(pdfPath, extractedXmlPath);
    }
}
