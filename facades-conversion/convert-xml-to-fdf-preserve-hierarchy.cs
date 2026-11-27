using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Base directory of the executable – makes the sample runnable anywhere
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output roots (allow overrides via command‑line arguments)
        string inputRoot = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0])
            ? args[0]
            : Path.Combine(baseDir, "InputXml");
        string outputRoot = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])
            ? args[1]
            : Path.Combine(baseDir, "OutputFdf");

        // Validate the input directory – if it does not exist, give a clear message and stop.
        if (!Directory.Exists(inputRoot))
        {
            Console.Error.WriteLine($"[Error] Input directory not found: '{inputRoot}'." +
                                    "\nPlace XML files in this folder or pass a valid path as the first argument.");
            return;
        }

        // Ensure the output root exists (create it if necessary)
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Recursively enumerate all XML files in the input tree
            foreach (string xmlFilePath in Directory.GetFiles(inputRoot, "*.xml", SearchOption.AllDirectories))
            {
                // Compute the path of the XML file relative to the input root
                string relativePath = Path.GetRelativePath(inputRoot, xmlFilePath);

                // Determine the directory in the output tree that mirrors the input hierarchy
                string outputDir = Path.Combine(outputRoot, Path.GetDirectoryName(relativePath) ?? string.Empty);
                Directory.CreateDirectory(outputDir);

                // Build the output FDF file name (same name, .fdf extension)
                string fdfFileName = Path.ChangeExtension(Path.GetFileName(relativePath), ".fdf");
                string fdfFilePath = Path.Combine(outputDir, fdfFileName);

                // Perform the conversion from XML to FDF – the API expects streams, not file paths
                using (FileStream xmlStream = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read))
                using (FileStream fdfStream = new FileStream(fdfFilePath, FileMode.Create, FileAccess.Write))
                {
                    FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
                }

                Console.WriteLine($"Converted: '{xmlFilePath}' → '{fdfFilePath}'");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[Unhandled Exception] {ex.GetType().Name}: {ex.Message}");
        }
    }
}
