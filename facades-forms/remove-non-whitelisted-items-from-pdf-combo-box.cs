using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Whitelist of allowed values for the "State" list field
        string[] whitelist = new[] { "CA", "NY", "TX" };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Retrieve the list box field named "State"
        if (!doc.Form.HasField("State"))
        {
            Console.Error.WriteLine("Field 'State' not found in the PDF.");
            return;
        }

        // The indexer returns a WidgetAnnotation; cast it to the concrete field type.
        ListBoxField? stateField = doc.Form["State"] as ListBoxField;
        if (stateField == null)
        {
            Console.Error.WriteLine("Field 'State' is not a list box field.");
            return;
        }

        // Determine which options need to be removed (those not in the whitelist)
        var toDelete = new List<string>();
        foreach (Option opt in stateField.Options)
        {
            bool allowed = whitelist.Any(w => string.Equals(opt.Name, w, StringComparison.OrdinalIgnoreCase));
            if (!allowed)
                toDelete.Add(opt.Value); // DeleteOption expects the export value
        }

        // Remove disallowed options
        foreach (string val in toDelete)
        {
            stateField.DeleteOption(val);
        }

        // Save the modified PDF
        doc.Save(outputPath);
    }
}
