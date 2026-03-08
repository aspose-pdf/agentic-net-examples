using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Define file paths – adjust as needed
        // -----------------------------------------------------------------
        const string pdfTemplate = "FormTemplate.pdf"; // PDF form template
        const string outputPdf   = "FilledForm.pdf";   // Resulting filled PDF

        // -----------------------------------------------------------------
        // 2. Prepare data to fill the PDF form.
        //    In the original example the data came from an Excel worksheet
        //    using Aspose.Cells. Because the project does not reference the
        //    Aspose.Cells assembly, we replace that part with a simple in‑memory
        //    dictionary. The keys must match the field names defined in the PDF.
        // -----------------------------------------------------------------
        var fieldValues = new Dictionary<string, string>
        {
            { "Name",    "John Doe" },
            { "Date",    DateTime.Today.ToShortDateString() },
            { "Address", "123 Main St, Anytown" }
            // Add additional field/value pairs as required.
        };

        if (!File.Exists(pdfTemplate))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplate}");
            return;
        }

        // -----------------------------------------------------------------
        // 3. Fill the PDF form using Aspose.Pdf.Facades.Form
        // -----------------------------------------------------------------
        using (Form pdfForm = new Form(pdfTemplate))
        {
            foreach (var kvp in fieldValues)
            {
                // Fill the field – returns true if the field exists and is filled.
                bool filled = pdfForm.FillField(kvp.Key, kvp.Value);
                if (!filled)
                {
                    Console.WriteLine($"Warning: Field '{kvp.Key}' not found in the PDF form.");
                }
            }

            // Save the filled PDF.
            pdfForm.Save(outputPdf);
        }

        Console.WriteLine($"Form filled and saved to '{Path.GetFullPath(outputPdf)}'.");
    }
}
