using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "flattened.pdf";   // PDF that was previously flattened
        const string outputPath = "restored.pdf";    // PDF after attempting to restore editability

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Check whether any form fields are present
            if (doc.Form.Count == 0)
            {
                // The document has no form fields – it was flattened.
                // Aspose.Pdf cannot recreate the original fields once they are flattened.
                // To edit the form you must start from the original (unflattened) PDF.
                Console.WriteLine("The document is flattened; form fields cannot be restored automatically.");
            }
            else
            {
                // The document still contains editable fields.
                // Example: enable auto‑restore in case some fields are missing.
                doc.Form.AutoRestoreForm = true;

                // Optionally make annotations independent (does not unflatten but ensures they are editable)
                foreach (Page page in doc.Pages)
                {
                    doc.Form.MakeFormAnnotationsIndependent(page);
                }

                // Save the (potentially modified) document
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
    }
}