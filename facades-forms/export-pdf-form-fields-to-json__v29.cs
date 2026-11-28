using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath  = "FormH.pdf";
        const string xmlPath  = "FormHData.xml";
        const string jsonPath = "FormHData.json";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Use the Form facade to work with PDF form data
            using (Form form = new Form())
            {
                // Bind the PDF document to the facade
                form.BindPdf(pdfPath);

                // Import XML data – the facade expects a Stream, not a file path string
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportXml(xmlStream);
                }

                // Export the current field values to JSON – also requires a Stream
                using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportJson(jsonStream);
                }
            }

            Console.WriteLine($"Form fields exported to JSON: {jsonPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
