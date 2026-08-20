using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Base directory of the application (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output directories relative to the base directory
        string inputDirectory = Path.Combine(baseDir, "XmlInputs");
        string outputDirectory = Path.Combine(baseDir, "PdfOutputs");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // If the input directory does not exist, create it and inform the user.
        // This prevents an unhandled DirectoryNotFoundException and makes the sample runnable out‑of‑the‑box.
        if (!Directory.Exists(inputDirectory))
        {
            Directory.CreateDirectory(inputDirectory);
            Console.WriteLine($"[INFO] Input directory not found. Created empty folder at '{inputDirectory}'. Place XML files there and re‑run the program.");
            return;
        }

        // Get all XML files in the input directory
        string[] xmlFiles = Directory.GetFiles(inputDirectory, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine($"[INFO] No XML files found in '{inputDirectory}'. Nothing to process.");
            return;
        }

        // Optional: create a simple log file to capture detailed steps and errors
        string logPath = Path.Combine(outputDirectory, "ProcessingLog.txt");
        var logBuilder = new StringBuilder();
        logBuilder.AppendLine($"Processing started at {DateTime.Now:O}");
        logBuilder.AppendLine($"Input directory : {inputDirectory}");
        logBuilder.AppendLine($"Output directory: {outputDirectory}");
        logBuilder.AppendLine();

        // Process each XML file individually
        foreach (string xmlPath in xmlFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
            string pdfPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".pdf");

            Console.WriteLine($"Processing '{xmlPath}' → '{pdfPath}'");
            logBuilder.AppendLine($"[START] {xmlPath} -> {pdfPath}");
            var startTime = DateTime.Now;

            try
            {
                // Initialize load options for XML conversion
                XmlLoadOptions loadOptions = new XmlLoadOptions();

                // Load the XML file and create a PDF document
                using (Document pdfDocument = new Document(xmlPath, loadOptions))
                {
                    // Save the generated PDF
                    pdfDocument.Save(pdfPath);
                }

                var duration = DateTime.Now - startTime;
                Console.WriteLine($"Successfully generated PDF: {pdfPath} (took {duration.TotalSeconds:F2}s)");
                logBuilder.AppendLine($"[SUCCESS] {pdfPath} (Duration: {duration.TotalSeconds:F2}s)");
            }
            catch (PdfException pdfEx)
            {
                // Log Aspose.Pdf specific errors
                Console.Error.WriteLine($"[PDF ERROR] File: {xmlPath}");
                Console.Error.WriteLine($"Message: {pdfEx.Message}");
                Console.Error.WriteLine($"StackTrace: {pdfEx.StackTrace}");

                logBuilder.AppendLine($"[PDF ERROR] File: {xmlPath}");
                logBuilder.AppendLine($"Message: {pdfEx.Message}");
                logBuilder.AppendLine($"StackTrace: {pdfEx.StackTrace}");
            }
            catch (Exception ex)
            {
                // Log any other unexpected errors
                Console.Error.WriteLine($"[GENERAL ERROR] File: {xmlPath}");
                Console.Error.WriteLine($"Message: {ex.Message}");
                Console.Error.WriteLine($"StackTrace: {ex.StackTrace}");

                logBuilder.AppendLine($"[GENERAL ERROR] File: {xmlPath}");
                logBuilder.AppendLine($"Message: {ex.Message}");
                logBuilder.AppendLine($"StackTrace: {ex.StackTrace}");
            }
            finally
            {
                logBuilder.AppendLine($"[END] {xmlPath}\n");
            }
        }

        logBuilder.AppendLine($"Processing completed at {DateTime.Now:O}");
        // Write the log to a file (overwrites previous log)
        File.WriteAllText(logPath, logBuilder.ToString());
        Console.WriteLine($"Detailed log written to: {logPath}");
        Console.WriteLine("Processing completed.");
    }
}
