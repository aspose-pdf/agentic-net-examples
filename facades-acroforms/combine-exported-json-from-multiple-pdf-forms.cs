using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;   // Facade classes (Form, etc.)

class Program
{
    static void Main()
    {
        // Input PDF files whose form data should be exported as JSON
        string[] pdfFiles = new string[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Output file that will contain a single JSON array with all fragments
        const string outputJsonPath = "combined.json";

        // Collect JSON fragments from each PDF
        List<string> jsonFragments = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Form facade loads the PDF and provides ExportJson method
            Form form = new Form(pdfPath);
            using (MemoryStream ms = new MemoryStream())
            {
                // Export the form fields of the current PDF to a memory stream as JSON
                form.ExportJson(ms, true); // indented output for readability
                ms.Position = 0; // reset stream position for reading

                using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
                {
                    string json = reader.ReadToEnd();
                    jsonFragments.Add(json);
                }
            }
        }

        // Combine all fragments into a single JSON array
        string combinedJson = "[" + string.Join(",", jsonFragments) + "]";

        // Write the combined JSON array to the output file
        using (FileStream outStream = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(outStream, Encoding.UTF8))
        {
            writer.Write(combinedJson);
        }

        Console.WriteLine($"Combined JSON written to '{outputJsonPath}'.");
    }
}