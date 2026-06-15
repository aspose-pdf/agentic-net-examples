using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string logPath = "xfdf_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the annotation editor with the loaded document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
            {
                // Export annotations to an XFDF file – use a FileStream because the API expects a Stream
                using (FileStream fileStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    editor.ExportAnnotationsToXfdf(fileStream);
                }

                // Export annotations to a memory stream to capture the XFDF content for logging
                using (MemoryStream ms = new MemoryStream())
                {
                    editor.ExportAnnotationsToXfdf(ms);
                    ms.Position = 0; // Reset stream position for reading

                    // Read the XFDF content as a string (UTF‑8 assumed)
                    string xfdfContent = new StreamReader(ms).ReadToEnd();

                    // Write the XFDF content to a log file (overwrite each run)
                    File.WriteAllText(logPath, xfdfContent);
                }
            }
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and logged to '{logPath}'.");
    }
}
