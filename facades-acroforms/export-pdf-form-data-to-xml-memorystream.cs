using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // MemoryStream will receive the exported XML data
        using (MemoryStream xmlStream = new MemoryStream())
        {
            // Initialize the Form facade with the PDF file
            using (Form form = new Form(pdfPath))
            {
                // Export the form fields to the memory stream as XML
                form.ExportXml(xmlStream);
            }

            // Reset the stream position to read the XML content
            xmlStream.Position = 0;

            // Example: read the XML into a string and display it
            using (StreamReader reader = new StreamReader(xmlStream))
            {
                string xmlContent = reader.ReadToEnd();
                Console.WriteLine("Exported XML:");
                Console.WriteLine(xmlContent);
            }
        }
    }
}