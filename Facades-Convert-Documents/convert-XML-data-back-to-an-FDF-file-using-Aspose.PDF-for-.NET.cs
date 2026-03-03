using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source XML and destination FDF files
        const string xmlPath = "input.xml";
        const string fdfPath = "output.fdf";

        // Verify that the XML file exists before proceeding
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Open the source XML stream for reading and the destination FDF stream for writing
            using (FileStream src = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            using (FileStream dest = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                // Convert the XML form data to FDF using the static FormDataConverter method
                FormDataConverter.ConvertXmlToFdf(src, dest);
            }

            Console.WriteLine($"XML successfully converted to FDF: {fdfPath}");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during conversion
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}