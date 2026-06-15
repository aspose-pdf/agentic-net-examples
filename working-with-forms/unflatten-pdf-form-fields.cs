using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "flattened.pdf";   // PDF that was previously flattened
        const string outputPath = "unflattened.pdf"; // PDF with editable form fields restored

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the flattened PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Enable automatic restoration of form fields from existing annotations
            doc.Form.AutoRestoreForm = true;

            // Iterate through all pages and make form annotations independent.
            // This helps recreate form field objects where possible.
            foreach (Page page in doc.Pages)
            {
                doc.Form.MakeFormAnnotationsIndependent(page);
            }

            // Save the document – the form fields should now be editable again.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Unflattened PDF saved to '{outputPath}'.");
    }
}