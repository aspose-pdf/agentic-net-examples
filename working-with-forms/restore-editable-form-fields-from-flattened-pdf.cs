using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "flattened_form.pdf";
        const string outputPath = "restored_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF that was previously flattened
        using (Document doc = new Document(inputPath))
        {
            // Enable automatic restoration of missing form fields from existing annotations
            doc.Form.AutoRestoreForm = true;

            // Iterate through all pages and make any form annotations independent.
            // This helps to recreate editable fields where possible.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                doc.Form.MakeFormAnnotationsIndependent(page);
            }

            // Save the document; fields are now editable again (if they could be reconstructed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Restored PDF saved to '{outputPath}'.");
    }
}