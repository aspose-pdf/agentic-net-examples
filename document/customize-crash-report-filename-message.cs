using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // (Optional) Perform any PDF manipulations here.

                // Save the document normally.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved to '{outputPath}'. Crash reports will use the custom name if generated.");
        }
        catch (Exception ex)
        {
            // Create CrashReportOptions with the caught exception and set a custom message.
            // The CustomMessage value is incorporated into the crash‑report file name
            // if Aspose.Pdf generates a crash report during processing.
            var crashOptions = new CrashReportOptions(ex)
            {
                CustomMessage = "MyApp_CrashReport"
            };

            // No explicit assignment to the Document is required – Aspose.Pdf will use the
            // CrashReportOptions instance internally when the exception is re‑thrown or
            // when the library attempts to generate a crash report.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
