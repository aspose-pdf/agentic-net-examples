using System;
using System.IO;
using Aspose.Pdf.Facades;

class FdfToXmlConverter
{
    /// <summary>
    /// Converts an FDF file to an XML representation using Aspose.Pdf.Facades.FormDataConverter.
    /// </summary>
    /// <param name="sourceFdfPath">Full path to the source .fdf file.</param>
    /// <param name="destinationXmlPath">Full path where the resulting .xml file will be saved.</param>
    public static void ConvertFdfToXml(string sourceFdfPath, string destinationXmlPath)
    {
        // Validate arguments
        if (string.IsNullOrWhiteSpace(sourceFdfPath))
            throw new ArgumentException("Source path is null or empty.", nameof(sourceFdfPath));
        if (string.IsNullOrWhiteSpace(destinationXmlPath))
            throw new ArgumentException("Destination path is null or empty.", nameof(destinationXmlPath));

        // Ensure the source file exists
        if (!File.Exists(sourceFdfPath))
            throw new FileNotFoundException("FDF file not found.", sourceFdfPath);

        // Ensure the output directory exists
        string outDir = Path.GetDirectoryName(destinationXmlPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // FormDataConverter.ConvertFdfToXml is a static method – call it directly.
        using (FileStream srcStream = new FileStream(sourceFdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream dstStream = new FileStream(destinationXmlPath, FileMode.Create, FileAccess.Write))
        {
            FormDataConverter.ConvertFdfToXml(srcStream, dstStream);
        }
    }

    // Simple entry point to make the project executable
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: FdfToXmlConverter <sourceFdfPath> <destinationXmlPath>");
            return;
        }

        try
        {
            ConvertFdfToXml(args[0], args[1]);
            Console.WriteLine($"Conversion succeeded: '{args[0]}' -> '{args[1]}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
