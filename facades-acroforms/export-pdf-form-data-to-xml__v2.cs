using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportFormDataToXml
{
    static void Main()
    {
        // Path to the PDF form file
        const string pdfPath = "PdfForm.pdf";

        // Create the Form facade for the PDF document
        using (Form form = new Form(pdfPath))
        {
            // MemoryStream will hold the exported XML data
            using (MemoryStream xmlStream = new MemoryStream())
            {
                // Export form fields to the XML stream (no intermediate file)
                form.ExportXml(xmlStream);

                // Reset stream position to read the content
                xmlStream.Position = 0;

                // Example: read the XML as a string (optional)
                using (StreamReader reader = new StreamReader(xmlStream))
                {
                    string xmlContent = reader.ReadToEnd();
                    Console.WriteLine("Exported XML:");
                    Console.WriteLine(xmlContent);
                }
            }
        }
    }
}