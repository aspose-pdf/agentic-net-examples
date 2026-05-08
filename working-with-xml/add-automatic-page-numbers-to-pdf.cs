using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace contains Document, XmlLoadOptions, etc.

class Program
{
    static void Main()
    {
        const string xmlInputPath = "input.xml";   // XML containing XSL‑FO with pagination logic
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Input XML not found: {xmlInputPath}");
            return;
        }

        try
        {
            // XmlLoadOptions resides directly in Aspose.Pdf namespace (no Aspose.Pdf.Xml sub‑namespace)
            var loadOptions = new XmlLoadOptions();

            // Load the XML/XSL‑FO document
            using (Document doc = new Document(xmlInputPath, loadOptions))
            {
                // Apply pagination defined in the XSL‑FO (page numbers, etc.)
                doc.Pages.UpdatePagination();

                // Save the resulting PDF
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"PDF with automatic page numbers saved to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}