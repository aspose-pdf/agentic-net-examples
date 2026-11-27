using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source FDF file and the target XML file
        const string fdfPath = "input.fdf";
        const string xmlPath = "output.xml";

        // Verify that the FDF file exists before attempting conversion
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // ConvertFdfToXml is a static method; call it via the class name.
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            using (FileStream xmlStream = File.Create(xmlPath))
            {
                FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
            }

            Console.WriteLine($"Conversion successful. XML saved to: {xmlPath}");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during conversion.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}