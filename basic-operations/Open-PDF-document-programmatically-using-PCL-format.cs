using System;
using System.IO;
using Aspose.Pdf; // PclLoadOptions resides in this namespace

class Program
{
    static void Main()
    {
        // Determine a concrete directory that contains the input files.
        // Here we assume a sub‑folder named "Data" next to the executable.
        string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        // Verify that the directory exists.
        if (!Directory.Exists(dataDir))
        {
            Console.WriteLine($"Data directory does not exist: {dataDir}");
            return;
        }

        // Build full paths for the source PCL and the target PDF.
        string pclFile = Path.Combine(dataDir, "input.pcl");
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Ensure the source PCL file is present before attempting conversion.
        if (!File.Exists(pclFile))
        {
            Console.WriteLine($"PCL source file not found: {pclFile}");
            return;
        }

        try
        {
            // Initialize PCL load options (default settings are sufficient for most cases).
            var pclLoadOptions = new PclLoadOptions();

            // Load the PCL file into a PDF Document using the constructor that accepts LoadOptions.
            using (var pdfDocument = new Document(pclFile, pclLoadOptions))
            {
                // Save the converted document as PDF.
                pdfDocument.Save(pdfFile);
            }

            Console.WriteLine($"PCL file has been converted and saved to: {pdfFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}