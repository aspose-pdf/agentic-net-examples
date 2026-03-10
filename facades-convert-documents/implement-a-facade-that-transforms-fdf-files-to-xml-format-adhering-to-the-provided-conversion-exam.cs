using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class FdfToXmlFacade
{
    /// <summary>
    /// Converts an FDF file to an XML file using Aspose.Pdf.Facades.FormDataConverter.
    /// </summary>
    /// <param name="sourceFdfPath">Full path to the source FDF file.</param>
    /// <param name="destXmlPath">Full path where the resulting XML file will be saved.</param>
    public static void Convert(string sourceFdfPath, string destXmlPath)
    {
        // Verify that the source file exists before attempting conversion.
        if (!File.Exists(sourceFdfPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found at '{sourceFdfPath}'.");
            return;
        }

        try
        {
            // Open the source FDF stream for reading and the destination XML stream for writing.
            // Using statements ensure deterministic disposal of the streams.
            using (FileStream sourceStream = new FileStream(sourceFdfPath, FileMode.Open, FileAccess.Read))
            using (FileStream destStream = new FileStream(destXmlPath, FileMode.Create, FileAccess.Write))
            {
                // Perform the conversion using the static method provided by Aspose.Pdf.Facades.
                FormDataConverter.ConvertFdfToXml(sourceStream, destStream);
            }

            Console.WriteLine($"Conversion successful. XML saved to '{destXmlPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected exceptions and report them.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}

// Entry point required for a console application.
public class Program
{
    /// <summary>
    /// Simple command‑line driver for the FdfToXmlFacade.
    /// Expected arguments: <sourceFdfPath> <destXmlPath>
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: FdfToXmlFacade <sourceFdfPath> <destXmlPath>");
            return;
        }

        string sourceFdfPath = args[0];
        string destXmlPath = args[1];

        FdfToXmlFacade.Convert(sourceFdfPath, destXmlPath);
    }
}
