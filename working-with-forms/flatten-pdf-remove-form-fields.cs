using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "plain.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Gather all form field names
                var fieldNames = doc.Form.Fields.Select(f => f.FullName).ToList();

                // Remove each field from the document
                foreach (var name in fieldNames)
                {
                    doc.Form.Delete(name);
                }

                // Save the non‑interactive PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Plain PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}