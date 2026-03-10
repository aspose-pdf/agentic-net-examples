using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "output.xml";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF using the Form facade (constructor binds the PDF)
        Form form = new Form(pdfPath);

        // Export the PDF form data (or entire content) to a memory stream as XML
        using (MemoryStream xmlStream = new MemoryStream())
        {
            form.ExportXml(xmlStream);

            // Reset the stream position to the beginning before reading
            xmlStream.Position = 0;

            // Persist the XML to disk using UTF-8 encoding
            using (StreamReader reader = new StreamReader(xmlStream, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(xmlPath, false, Encoding.UTF8))
            {
                writer.Write(reader.ReadToEnd());
            }
        }

        Console.WriteLine($"XML successfully saved to '{xmlPath}'.");
    }
}