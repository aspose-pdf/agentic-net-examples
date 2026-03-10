using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input XML file containing form data (exported from a PDF form)
        const string xmlInputPath = "formData.xml";
        // Output FDF file that can be imported into a PDF form
        const string fdfOutputPath = "formData.fdf";

        // Validate input file existence
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Input XML file not found: {xmlInputPath}");
            return;
        }

        // Use FileStream inside using blocks to ensure deterministic disposal
        using (FileStream xmlStream = new FileStream(xmlInputPath, FileMode.Open, FileAccess.Read))
        using (FileStream fdfStream = new FileStream(fdfOutputPath, FileMode.Create, FileAccess.Write))
        {
            // Convert the XML form data to FDF format.
            // FormDataConverter.ConvertXmlToFdf is a static method that reads from the source XML stream
            // and writes the resulting FDF to the destination stream.
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
        }

        Console.WriteLine($"XML data successfully converted to FDF: {fdfOutputPath}");
    }
}