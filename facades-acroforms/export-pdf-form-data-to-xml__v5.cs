using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "form_data.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document pdfDocument = new Document(inputPdf))
            using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                // Use the Form facade to export form data. The facade's ExportFdf method writes the
                // form data in FDF format, which is an XML‑based representation of the fields.
                using (Form formFacade = new Form(pdfDocument))
                {
                    formFacade.ExportFdf(xmlStream);
                }
            }

            Console.WriteLine($"Form data successfully exported to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
