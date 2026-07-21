using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // NOTE: Adjust these paths as needed for your environment.
    private const string InputDir = @"C:\InputXml";
    private const string OutputDir = @"C:\OutputPdf";
    private const string LogDir = @"C:\ValidationLogs";

    static void Main()
    {
        // Ensure output and log directories exist.
        Directory.CreateDirectory(OutputDir);
        Directory.CreateDirectory(LogDir);

        // Verify the input directory exists before enumerating files.
        if (!Directory.Exists(InputDir))
        {
            Console.Error.WriteLine($"Input directory '{InputDir}' does not exist. No files to process.");
            return;
        }

        // Process each XML file in the input directory.
        foreach (string xmlFile in Directory.GetFiles(InputDir, "*.xml"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlFile);
            string pdfPath = Path.Combine(OutputDir, fileNameWithoutExt + ".pdf");
            string logPath = Path.Combine(LogDir, fileNameWithoutExt + "_validation.log");
            string stepLogPath = Path.Combine(LogDir, fileNameWithoutExt + "_steps.log");

            var stepLog = new StringBuilder();
            stepLog.AppendLine($"[{DateTime.Now:O}] Starting processing of '{xmlFile}'.");

            try
            {
                stepLog.AppendLine($"[{DateTime.Now:O}] Initializing XmlLoadOptions.");
                XmlLoadOptions loadOptions = new XmlLoadOptions();

                stepLog.AppendLine($"[{DateTime.Now:O}] Loading XML as PDF document.");
                using (Document pdfDocument = new Document(xmlFile, loadOptions))
                {
                    stepLog.AppendLine($"[{DateTime.Now:O}] Document loaded successfully. Beginning validation.");

                    // Validate the generated PDF against PDF/A‑1B and write validation messages to logPath.
                    bool isValid = pdfDocument.Validate(logPath, PdfFormat.PDF_A_1B);
                    stepLog.AppendLine($"[{DateTime.Now:O}] Validation result: {(isValid ? "succeeded" : "failed")}. Validation log written to '{logPath}'.");

                    stepLog.AppendLine($"[{DateTime.Now:O}] Saving PDF to '{pdfPath}'.");
                    pdfDocument.Save(pdfPath);
                    stepLog.AppendLine($"[{DateTime.Now:O}] PDF saved successfully.");
                }
            }
            catch (PdfException pdfEx)
            {
                string msg = $"PdfException while processing '{xmlFile}': {pdfEx.Message}";
                Console.Error.WriteLine(msg);
                stepLog.AppendLine($"[{DateTime.Now:O}] ERROR: {msg}");
            }
            catch (Exception ex)
            {
                string msg = $"Error while processing '{xmlFile}': {ex.Message}";
                Console.Error.WriteLine(msg);
                stepLog.AppendLine($"[{DateTime.Now:O}] ERROR: {msg}");
            }
            finally
            {
                // Write the step‑by‑step log for this file.
                try
                {
                    File.WriteAllText(stepLogPath, stepLog.ToString());
                }
                catch (Exception logEx)
                {
                    Console.Error.WriteLine($"Failed to write step log to '{stepLogPath}': {logEx.Message}");
                }
            }

            Console.WriteLine(); // Blank line for readability between files.
        }

        Console.WriteLine("Processing completed.");
    }
}
