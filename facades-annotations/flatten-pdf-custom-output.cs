using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDirectory = "flattened";
        const string outputFileName = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Ensure the output directory exists
            DirectoryInfo outputDirInfo = new DirectoryInfo(outputDirectory);
            if (!outputDirInfo.Exists)
            {
                outputDirInfo.Create();
            }

            // Switch the current working directory to the output directory
            string previousDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = outputDirInfo.FullName;

            using (Document doc = new Document(inputPath))
            {
                // Flatten the PDF (remove form fields and annotations)
                doc.Flatten();

                // Save using a simple filename (no directory path as per style rule)
                doc.Save(outputFileName);
            }

            // Restore the original working directory (optional)
            Environment.CurrentDirectory = previousDir;

            Console.WriteLine($"Flattened PDF saved to '{Path.Combine(outputDirInfo.FullName, outputFileName)}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
