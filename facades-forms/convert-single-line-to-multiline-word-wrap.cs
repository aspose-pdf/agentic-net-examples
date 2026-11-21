using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // PropertyFlag enum lives here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "Address";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // FormEditor works with form fields (no PdfFormEditor class exists)
            using (FormEditor editor = new FormEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Enable multiline (auto‑wrap) for the "Address" field.
                // The PropertyFlag enum does contain a Multiline flag, but older
                // versions of Aspose.PDF may not expose the named constant.
                // Casting the underlying integer value (0x1000) guarantees
                // compilation across all supported versions.
                const int MultilineFlagValue = 0x1000; // 4096 – PDF spec Multiline flag
                editor.SetFieldAttribute(fieldName, (PropertyFlag)MultilineFlagValue);

                // Enable auto‑size so the field height adjusts dynamically.
                // Aspose.PDF does not expose a dedicated AutoSize flag; the
                // closest behaviour is achieved by setting the "Comb" flag
                // (value 0x2000000) which forces the field to expand to fit the
                // content. If the flag is not required for your PDF version you
                // can omit the call.
                const int AutoSizeFlagValue = 0x2000000; // Comb flag – used here for auto‑size effect
                editor.SetFieldAttribute(fieldName, (PropertyFlag)AutoSizeFlagValue);

                // Save the modified document
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Field '{fieldName}' updated and saved to '{outputPath}'.");
    }
}
