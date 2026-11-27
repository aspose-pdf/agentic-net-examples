using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the input FDF file and the desired output XML file
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
            // ConvertFdfToXml is a static method – call it on the type, not on an instance
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
            }

            Console.WriteLine($"Conversion successful. XML saved to: {xmlPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during conversion
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
