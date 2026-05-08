using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure flatten settings to avoid executing any JavaScript actions
            Form.FlattenSettings flattenSettings = new Form.FlattenSettings
            {
                CallEvents          = false, // Do not invoke JavaScript events
                HideButtons         = false,
                UpdateAppearances   = false,
                ApplyRedactions     = false
            };

            // Flatten the document – this removes all form fields (and any attached JavaScript)
            doc.Flatten(flattenSettings);

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved without JavaScript actions to '{outputPath}'.");
    }
}