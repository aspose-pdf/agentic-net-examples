using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string fdfPath = "input.fdf";
        const string xmlPath = "output.xml";

        // Verify the source FDF file exists
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // ConvertFdfToXml is a static method; call it via the class name
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
            }

            Console.WriteLine($"Conversion successful. XML saved to: {xmlPath}");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during conversion
            Console.Error.WriteLine($"Error converting FDF to XML: {ex.Message}");
        }
    }
}