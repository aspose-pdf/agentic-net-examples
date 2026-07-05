using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input_form.pdf";

        // Verify that the PDF form exists
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
                // Export the form fields to the memory stream (no intermediate file)
                form.ExportXml(xmlStream);

                // Reset the stream position to read from the beginning
                xmlStream.Position = 0;

                // Optional: read the XML content as a string and output it
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