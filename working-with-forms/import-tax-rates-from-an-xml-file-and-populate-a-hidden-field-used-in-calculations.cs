using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";   // PDF with a hidden field for tax rates
        const string xmlDataPath     = "taxrates.xml";   // XML containing the tax rate values
        const string outputPdfPath   = "output.pdf";     // Resulting PDF

        // Verify input files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            // Initialize the Form facade on the loaded document
            using (Form form = new Form(pdfDoc))
            {
                // Import XML data into the form fields
                using (FileStream xmlStream = File.OpenRead(xmlDataPath))
                {
                    form.ImportXml(xmlStream);
                }

                // If you need to set a hidden field explicitly (optional)
                // Example: hidden field named "TaxRateHidden"
                // form.FillField("TaxRateHidden", "0.075"); // uncomment and adjust as needed

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF with imported tax rates saved to '{outputPdfPath}'.");
    }
}