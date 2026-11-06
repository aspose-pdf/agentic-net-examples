using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form_input.pdf";
        const string outputPdf = "form_output.pdf";
        const string exportedDataPath = "form_data.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a Form facade bound to the document
            using (Form form = new Form(doc))
            {
                // Export current form field values to a memory stream (FDF format)
                using (MemoryStream exportStream = new MemoryStream())
                {
                    // Export in FDF format – the facade handles UTF‑8 internally
                    form.ExportFdf(exportStream);
                    exportStream.Position = 0; // Reset for reading

                    // Optionally write the exported data to a file for inspection
                    using (FileStream fileOut = new FileStream(exportedDataPath, FileMode.Create, FileAccess.Write))
                    {
                        exportStream.CopyTo(fileOut);
                    }

                    // Re‑import the same data (could be modified) back into the PDF
                    exportStream.Position = 0; // Ensure stream is at the beginning
                    form.ImportFdf(exportStream);
                }

                // Save the PDF with updated form field values
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine("Form data exported and re‑imported using UTF‑8 encoding via FDF.");
    }
}
