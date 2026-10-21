using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = { "form1.pdf", "form2.pdf" };
        // Path to the diagnostic log file
        const string logPath = "xfdf_log.txt";

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Export XFDF by using the filename‑only overload (no SaveFormat enum required)
                string xfdfPath = Path.ChangeExtension(pdfPath, ".xfdf");
                pdfDoc.Save(xfdfPath); // format inferred from ".xfdf" extension

                // Read the exported XFDF content
                string xfdfContent = File.ReadAllText(xfdfPath);

                // Write the full XFDF content to the diagnostic log
                using (StreamWriter logWriter = new StreamWriter(logPath, true))
                {
                    logWriter.WriteLine($"--- XFDF export for '{pdfPath}' at {DateTime.Now:u} ---");
                    logWriter.WriteLine(xfdfContent);
                    logWriter.WriteLine(); // separator
                }

                Console.WriteLine($"Exported XFDF for '{pdfPath}' and logged diagnostic info.");
            }
        }
    }
}
