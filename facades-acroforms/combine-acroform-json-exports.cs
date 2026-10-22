using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // Included as per requirement

class Program
{
    static void Main()
    {
        // Input PDF files whose data will be exported to JSON
        string[] pdfFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };
        // Output JSON file that will contain a single JSON array
        const string outputJsonPath = "combined.json";

        // Validate input files
        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                return;
            }
        }

        // List to hold individual JSON objects as strings
        List<string> jsonObjects = new List<string>();

        try
        {
            // Extract text from each PDF and build a JSON object
            foreach (string pdfPath in pdfFiles)
            {
                using (Document doc = new Document(pdfPath))
                {
                    // Extract all text from the document
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages.Accept(absorber);
                    string rawText = absorber.Text ?? string.Empty;

                    // Escape characters for valid JSON string values
                    string escapedText = rawText
                        .Replace("\\", "\\\\")
                        .Replace("\"", "\\\"")
                        .Replace("\r", "")
                        .Replace("\n", "\\n");

                    // Build a simple JSON object containing the file name and its text
                    string jsonObject = $"{{\"file\":\"{Path.GetFileName(pdfPath)}\",\"text\":\"{escapedText}\"}}";
                    jsonObjects.Add(jsonObject);
                }
            }

            // Combine all objects into a single JSON array
            string combinedJson = "[" + string.Join(",", jsonObjects) + "]";

            // Write the combined JSON array to the output file
            File.WriteAllText(outputJsonPath, combinedJson);
            Console.WriteLine($"Combined JSON written to '{outputJsonPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}