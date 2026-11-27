using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string fdfPath = "output.fdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // ConvertXmlToFdf is a static method; call it on the type directly.
        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
        {
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
        }

        Console.WriteLine($"FDF file created at '{fdfPath}'.");
    }
}
