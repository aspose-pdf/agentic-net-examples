using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input XML file containing form data
        const string xmlFilePath = "input.xml";
        // Output FDF file that will be generated
        const string fdfFilePath = "output.fdf";

        // Verify that the source XML file exists
        if (!File.Exists(xmlFilePath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlFilePath}'.");
            return;
        }

        try
        {
            // Open the XML file for reading and create the FDF file for writing
            using (FileStream xmlStream = File.OpenRead(xmlFilePath))
            using (FileStream fdfStream = File.Create(fdfFilePath))
            {
                // Convert XML to FDF using Aspose.Pdf.Facades.FormDataConverter
                FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
            }

            Console.WriteLine($"Conversion successful. FDF saved to '{fdfFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}