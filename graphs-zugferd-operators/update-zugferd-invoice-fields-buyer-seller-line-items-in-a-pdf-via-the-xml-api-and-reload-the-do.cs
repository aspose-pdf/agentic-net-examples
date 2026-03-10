using System;
using System.IO;
using System.Linq; // Needed for Count() extension method
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfInputPath   = "invoice.pdf";          // ZUGFeRD PDF
        const string xmlDataPath    = "invoice.xml";          // ZUGFeRD XML data
        const string pdfOutputPath  = "invoice_updated.pdf";  // Updated PDF

        // Verify files exist
        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlDataPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfInputPath))
            {
                // Load the ZUGFeRD XML into a memory stream
                using (FileStream xmlStream = File.OpenRead(xmlDataPath))
                {
                    // Bind the XML data to the PDF (updates buyer, seller, line items, etc.)
                    pdfDoc.BindXml(xmlStream);
                }

                // Save the updated PDF
                pdfDoc.Save(pdfOutputPath);
            }

            // Reload the updated PDF to confirm changes
            using (Document reloadedDoc = new Document(pdfOutputPath))
            {
                // Example: output number of form fields after XML binding
                int fieldCount = 0;
                var fields = reloadedDoc.Form?.Fields;
                if (fields != null)
                {
                    // Count() is an extension method from System.Linq
                    fieldCount = fields.Count();
                }
                Console.WriteLine($"Reloaded PDF contains {fieldCount} form fields.");
            }

            Console.WriteLine($"ZUGFeRD invoice updated and saved to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
