using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Load the XFA form template
        using (Document pdf = new Document(templatePath))
        {
            // Verify that the document actually contains an XFA form
            if (!pdf.Form.HasXfa)
            {
                Console.Error.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // Fill form fields via the XFA indexer (path‑based)
            // Adjust field names to match those defined in the template
            pdf.Form.XFA["firstName"]          = "John";
            pdf.Form.XFA["lastName"]           = "Doe";
            pdf.Form.XFA["address.street"]     = "123 Main St";
            pdf.Form.XFA["address.city"]       = "Metropolis";
            pdf.Form.XFA["address.postalCode"] = "12345";

            // Flatten the form – field values become part of the page content
            pdf.Form.Flatten();

            // Save the resulting PDF
            pdf.Save(outputPath);
        }

        Console.WriteLine($"XFA form filled and saved to '{outputPath}'.");
    }
}
