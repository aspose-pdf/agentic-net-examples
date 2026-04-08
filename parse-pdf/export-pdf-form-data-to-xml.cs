using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file that contains form fields.
        const string inputPdfPath = "input.pdf";

        // Verify that the source file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Use the Facade Form class to work with form data.
        using (Form form = new Form())
        {
            // Bind the PDF document to the Form facade.
            form.BindPdf(inputPdfPath);

            // Create a memory stream that will receive the exported XML.
            using (MemoryStream xmlStream = new MemoryStream())
            {
                // Export the form data directly to the memory stream as XML.
                form.ExportXml(xmlStream);

                // Rewind the stream so it can be read from the beginning.
                xmlStream.Position = 0;

                // Example: read the XML content from the stream and display it.
                using (StreamReader reader = new StreamReader(xmlStream))
                {
                    string xmlContent = reader.ReadToEnd();
                    Console.WriteLine("Exported Form XML:");
                    Console.WriteLine(xmlContent);
                }
            }
        }
    }
}