using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormDataConverter resides here

class Program
{
    static void Main()
    {
        // Paths to the source XML file and the target FDF file
        const string xmlPath = "formData.xml";
        const string fdfPath = "output.fdf";

        // Validate input file existence
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        try
        {
            // Open the XML input stream for reading
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            // Create the FDF output stream for writing
            using (FileStream fdfStream = File.Create(fdfPath))
            {
                // Convert XML form data to FDF.
                // ConvertXmlToFdf is a static method, so call it via the type name.
                FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
            }

            Console.WriteLine($"Successfully converted '{xmlPath}' to FDF file '{fdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}