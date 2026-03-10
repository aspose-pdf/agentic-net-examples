using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source XML file and the destination FDF file
        const string xmlPath = "input.xml";
        const string fdfPath = "output.fdf";

        // Verify that the source XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Source XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Open the source XML stream for reading and the destination FDF stream for writing
            using (FileStream srcXml = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            using (FileStream destFdf = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                // Convert the XML form data to FDF using the Facades API
                FormDataConverter.ConvertXmlToFdf(srcXml, destFdf);
            }

            Console.WriteLine($"XML successfully converted to FDF: {fdfPath}");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during conversion
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}