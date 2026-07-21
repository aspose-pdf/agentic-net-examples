using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "form_filled.pdf";   // PDF after user has completed the form
        const string outputPdf = "form_updated.pdf";  // PDF with Summary field updated

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the Form facade on the loaded document
            using (Form form = new Form(doc))
            {
                // Retrieve the value from the "Notes" field
                object notesObj = form.GetField("Notes");
                string notesText = notesObj?.ToString() ?? string.Empty;

                // Copy the retrieved text into the "Summary" field
                form.FillField("Summary", notesText);

                // Save the modified document
                form.Save(outputPdf);
            }
        }

        Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");
    }
}