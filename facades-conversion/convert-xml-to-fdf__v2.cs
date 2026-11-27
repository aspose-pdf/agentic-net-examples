using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point for demonstration; expects two arguments: source XML and destination FDF paths.
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: <sourceXmlPath> <destinationFdfPath>");
            return;
        }

        ConvertXmlToFdf(args[0], args[1]);
    }

    // Performs XML‑to‑FDF conversion using Aspose.Pdf.Facades.FormDataConverter.
    static void ConvertXmlToFdf(string sourceXmlPath, string destinationFdfPath)
    {
        // Validate source file existence.
        if (!File.Exists(sourceXmlPath))
        {
            Console.Error.WriteLine($"Source XML file not found: {sourceXmlPath}");
            return;
        }

        // Open the source XML as a read‑only stream and the destination FDF as a writeable stream.
        // The FormDataConverter API expects Stream objects, not file path strings.
        using (FileStream xmlStream = File.OpenRead(sourceXmlPath))
        using (FileStream fdfStream = File.Create(destinationFdfPath))
        {
            // Convert the XML form data to an FDF file.
            // ConvertXmlToFdf is a static method, so call it on the type name.
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
        }

        Console.WriteLine($"FDF file successfully created at: {destinationFdfPath}");
    }
}
