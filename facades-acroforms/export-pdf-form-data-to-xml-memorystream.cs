using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF form (must exist on disk)
        const string pdfPath = "input_form.pdf";

        // Ensure the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create the Form facade for the PDF document
        // Using the constructor that accepts a file path
        Form pdfForm = new Form(pdfPath);

        // Prepare a memory stream to receive the exported XML
        using (MemoryStream xmlStream = new MemoryStream())
        {
            // Export form field data to the memory stream (no intermediate file)
            pdfForm.ExportXml(xmlStream);

            // Reset the stream position to the beginning for reading
            xmlStream.Position = 0;

            // Optionally, read the XML content as a string (e.g., for further processing)
            using (StreamReader reader = new StreamReader(xmlStream))
            {
                string xmlContent = reader.ReadToEnd();
                Console.WriteLine("Exported XML:");
                Console.WriteLine(xmlContent);
            }
        }

        // Clean up the Form facade
        pdfForm.Close();
    }
}