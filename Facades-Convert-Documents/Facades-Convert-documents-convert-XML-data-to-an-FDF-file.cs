using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source XML data and the target FDF file.
        const string xmlPath = "data.xml";
        const string fdfPath = "output.fdf";

        // Verify that the XML file exists before proceeding.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        try
        {
            // Open the XML file for reading and the FDF file for writing.
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                // Convert the XML data to FDF format.
                FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
            }

            Console.WriteLine($"Successfully converted '{xmlPath}' to FDF file '{fdfPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors during conversion.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}