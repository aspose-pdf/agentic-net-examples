using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade with the PDF file
        using (Form form = new Form(pdfPath))
        {
            // Create a memory stream to hold the exported XML
            using (MemoryStream xmlStream = new MemoryStream())
            {
                // Export form field data to the memory stream (no disk file created)
                form.ExportXml(xmlStream);

                // Reset stream position to the beginning for reading
                xmlStream.Position = 0;

                // Read the XML content from the memory stream
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