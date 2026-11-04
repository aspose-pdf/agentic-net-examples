using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Required for Form class

class Program
{
    static void Main()
    {
        const string inputPath = "flattened_form.pdf";
        const string outputPath = "unflattened_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document contains a form
                if (doc.Form != null)
                {
                    // NOTE: Aspose.PDF does not provide an API to "unflatten" a PDF that has already been flattened.
                    // Once a form is flattened the original field objects are removed from the document.
                    // To obtain an editable form you must work with the original (unflattened) PDF or a copy that
                    // still contains the AcroForm. Therefore the previous attempt to set a non‑existent
                    // IsFlattened property has been removed.
                    // If you need to ensure fields are editable, load the original template PDF instead of a
                    // flattened version.

                    // Example modification after loading the (still editable) PDF (optional)
                    // if (doc.Form["Name"] != null)
                    // {
                    //     doc.Form["Name"].Value = "John Doe";
                    // }
                }

                // Save the PDF (unchanged if it was already flattened, or edited if it was still editable)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
