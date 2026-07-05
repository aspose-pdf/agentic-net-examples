using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string xfdfOutputPath = "annotations.xfdf";
        const string logFilePath = "xfdf_export_log.txt";

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Create the PdfAnnotationEditor facade and bind the PDF document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Export all annotations to an XFDF file
            using (FileStream xfdfStream = new FileStream(xfdfOutputPath, FileMode.Create, FileAccess.Write))
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);
            }

            // Read the generated XFDF content
            string xfdfContent = File.ReadAllText(xfdfOutputPath);

            // Append the XFDF content to the diagnostic log file with a timestamp
            using (StreamWriter logWriter = new StreamWriter(logFilePath, append: true))
            {
                logWriter.WriteLine($"--- XFDF Export Log ---");
                logWriter.WriteLine($"Source PDF : {Path.GetFileName(inputPdfPath)}");
                logWriter.WriteLine($"Exported   : {Path.GetFileName(xfdfOutputPath)}");
                logWriter.WriteLine($"Timestamp  : {DateTime.Now:O}");
                logWriter.WriteLine("XFDF Content:");
                logWriter.WriteLine(xfdfContent);
                logWriter.WriteLine(new string('-', 80));
            }
        }

        Console.WriteLine("Export completed. XFDF written to file and logged.");
    }
}