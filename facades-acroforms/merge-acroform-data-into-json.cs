using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files containing form fields
        string[] pdfFiles = new string[]
        {
            "form1.pdf",
            "form2.pdf",
            "form3.pdf"
        };

        // List to hold JSON representation of each PDF's form data
        List<string> jsonFragments = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Bind the PDF to the Form facade
            using (Form form = new Form())
            {
                form.BindPdf(pdfPath);

                // Export form fields to JSON (as a stream)
                using (MemoryStream ms = new MemoryStream())
                {
                    // The second parameter indicates whether to export button fields (false = exclude)
                    form.ExportJson(ms, false);
                    ms.Position = 0;
                    string json = Encoding.UTF8.GetString(ms.ToArray());

                    // Trim whitespace to ensure proper concatenation later
                    json = json.Trim();
                    jsonFragments.Add(json);
                }
            }
        }

        // Combine all JSON fragments into a single JSON array
        string combinedJson = "[" + string.Join(",", jsonFragments) + "]";

        // Output file path for the aggregated JSON
        string outputJsonPath = "merged_form_data.json";

        // Write the combined JSON to the file
        File.WriteAllText(outputJsonPath, combinedJson, Encoding.UTF8);

        Console.WriteLine($"Merged form data saved to '{outputJsonPath}'.");
    }
}