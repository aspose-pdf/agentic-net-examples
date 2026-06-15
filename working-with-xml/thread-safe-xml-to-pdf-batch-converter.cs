using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf; // Core API – load option classes (e.g., XmlLoadOptions) are in this namespace

#pragma warning disable NU1903 // Suppress known high‑severity vulnerability warning for Microsoft.Bcl.Memory

// Thread‑safe batch conversion of XML files to PDF
public static class XmlToPdfBatchConverter
{
    // Converts each XML file in <xmlFilePaths> to a PDF placed in <outputFolder>.
    // The method runs conversions in parallel, each on its own thread,
    // and guarantees that every Document instance is created, used and disposed
    // within the same thread (no shared mutable state).
    public static async Task ConvertAllAsync(IEnumerable<string> xmlFilePaths, string outputFolder)
    {
        if (xmlFilePaths == null) throw new ArgumentNullException(nameof(xmlFilePaths));
        if (string.IsNullOrWhiteSpace(outputFolder)) throw new ArgumentException("Output folder must be specified.", nameof(outputFolder));

        Directory.CreateDirectory(outputFolder);

        // Use Task.Run for each file to keep the async signature.
        var conversionTasks = new List<Task>();

        foreach (var xmlPath in xmlFilePaths)
        {
            // Capture the current path for the lambda
            string currentXmlPath = xmlPath;

            conversionTasks.Add(Task.Run(() =>
            {
                // Validate input file existence
                if (!File.Exists(currentXmlPath))
                {
                    Console.Error.WriteLine($"XML file not found: {currentXmlPath}");
                    return;
                }

                // Derive PDF file name from XML file name
                string pdfFileName = Path.GetFileNameWithoutExtension(currentXmlPath) + ".pdf";
                string pdfPath = Path.Combine(outputFolder, pdfFileName);

                try
                {
                    // Load XML with explicit XmlLoadOptions (required by Aspose.Pdf)
                    using (Document pdfDocument = new Document(currentXmlPath, new XmlLoadOptions()))
                    {
                        // Save as PDF – no SaveOptions needed for PDF output
                        pdfDocument.Save(pdfPath);
                    }

                    Console.WriteLine($"Converted '{currentXmlPath}' → '{pdfPath}'");
                }
                catch (Exception ex)
                {
                    // Log conversion errors without terminating other tasks
                    Console.Error.WriteLine($"Error converting '{currentXmlPath}': {ex.Message}");
                }
            }));
        }

        // Await all conversions to finish
        await Task.WhenAll(conversionTasks);
    }
}

// Simple console entry point to satisfy the project’s requirement for a Main method.
public class Program
{
    // Async Main is supported in C# 7.1 and later.
    public static async Task Main(string[] args)
    {
        // Expect two arguments: input folder and output folder.
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: <app> <input-xml-folder> <output-pdf-folder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Get all *.xml files in the input folder (non‑recursive for simplicity).
        var xmlFiles = Directory.GetFiles(inputFolder, "*.xml");

        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found to convert.");
            return;
        }

        await XmlToPdfBatchConverter.ConvertAllAsync(xmlFiles, outputFolder);
    }
}
#pragma warning restore NU1903 // Re‑enable the warning for the rest of the project