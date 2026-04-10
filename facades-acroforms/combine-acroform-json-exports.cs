using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files whose form data will be exported to JSON
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Output file that will contain the combined JSON array
        const string outputJsonPath = "combined.json";

        // Collect individual JSON fragments
        List<string> jsonFragments = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Bind the PDF file to the Facades Form object
            using (Form form = new Form(pdfPath))
            {
                // Export the form fields to a memory stream as JSON
                using (MemoryStream ms = new MemoryStream())
                {
                    // ExportJson writes JSON; 'true' makes the output indented
                    form.ExportJson(ms, true);
                    ms.Position = 0; // Reset stream position for reading

                    using (StreamReader reader = new StreamReader(ms))
                    {
                        string json = reader.ReadToEnd().Trim();

                        // The ExportJson method may return a JSON array.
                        // If it does, strip the outer brackets so we can merge them.
                        if (json.StartsWith("[") && json.EndsWith("]"))
                        {
                            json = json.Substring(1, json.Length - 2).Trim();
                        }

                        jsonFragments.Add(json);
                    }
                }
            }
        }

        // Combine all fragments into a single JSON array
        string combinedJson = "[" + string.Join(",", jsonFragments) + "]";

        // Write the combined JSON to the output file
        File.WriteAllText(outputJsonPath, combinedJson);
        Console.WriteLine($"Combined JSON saved to '{outputJsonPath}'.");
    }
}