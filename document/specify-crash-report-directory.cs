using System;
using System.IO;
using Aspose.Pdf; // CrashReportOptions and PdfException are defined here

class Program
{
    static void Main()
    {
        try
        {
            // Simulate an exception that will be the basis of the crash report
            Exception sampleException = new InvalidOperationException("Sample exception for crash report generation.");

            // Create CrashReportOptions using the exception
            CrashReportOptions crashOptions = new CrashReportOptions(sampleException);

            // Define a custom directory where the report should be saved
            string reportDirectory = Path.Combine(Environment.CurrentDirectory, "CrashReports");
            Directory.CreateDirectory(reportDirectory); // Ensure the directory exists
            crashOptions.CrashReportDirectory = reportDirectory;

            // Optional: add a custom message to the report
            crashOptions.CustomMessage = "Additional diagnostic information can be placed here.";

            // Generate the HTML crash report
            PdfException.GenerateCrashReport(crashOptions);

            // Verify that the report file was created successfully
            string reportPath = crashOptions.CrashReportPath;
            if (File.Exists(reportPath))
            {
                Console.WriteLine($"Crash report generated at: {reportPath}");
            }
            else
            {
                Console.Error.WriteLine("Crash report generation failed: file not found.");
            }
        }
        catch (DllNotFoundException dllEx)
        {
            // This exception is thrown when the native Aspose.Pdf DLL (e.g., AsposePdfApi_*.dll) cannot be located.
            // Ensure that the native DLL is copied to the output folder or is reachable via the PATH environment variable.
            Console.Error.WriteLine($"Native Aspose.Pdf library not found: {dllEx.Message}");
        }
        catch (Exception ex)
        {
            // General exception handling – any unexpected error will be reported here.
            Console.Error.WriteLine($"An error occurred while generating the crash report: {ex.Message}");
        }
    }
}
