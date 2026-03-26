using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xmlPath = "invoice.xml";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Embed the ZUGFeRD XML into the PDF catalog (AF entry)
                pdfDoc.BindXml(xmlPath);

                // GetCatalogValue returns an object; cast/convert it to string safely
                object afObj = pdfDoc.GetCatalogValue("AF");
                string afEntry = afObj != null ? afObj.ToString() : null;

                if (!string.IsNullOrEmpty(afEntry))
                {
                    Console.WriteLine("ZUGFeRD XML successfully embedded. Catalog AF entry found.");
                }
                else
                {
                    Console.WriteLine("Embedding may have failed: Catalog AF entry not found.");
                }

                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Saved PDF with embedded XML to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
