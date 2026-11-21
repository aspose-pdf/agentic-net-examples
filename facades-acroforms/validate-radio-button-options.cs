using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF containing the form
        const string pdfPath = "form.pdf";

        // Name of the radio button field to inspect
        const string radioFieldName = "Gender";

        // Expected set of options for the radio button
        var expectedOptions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Male",
            "Female",
            "Other"
        };

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (FormEditor does not expose option values)
        Document pdfDocument = new Document(pdfPath);

        // Retrieve the radio button field from the document's form collection
        RadioButtonField radioField = pdfDocument.Form[radioFieldName] as RadioButtonField;
        if (radioField == null)
        {
            Console.Error.WriteLine($"Radio button field '{radioFieldName}' not found or is not a radio button.");
            return;
        }

        // Extract the option values (Value if set, otherwise the display name)
        string[] options = radioField.Options
                                      .Select(opt => !string.IsNullOrEmpty(opt.Value) ? opt.Value : opt.Name)
                                      .ToArray();

        if (options == null || options.Length == 0)
        {
            Console.Error.WriteLine($"Radio button field '{radioFieldName}' has no options.");
            return;
        }

        // Convert the retrieved options to a set for comparison
        var actualOptions = new HashSet<string>(options, StringComparer.OrdinalIgnoreCase);

        // Compare the actual options with the expected set
        bool matches = expectedOptions.SetEquals(actualOptions);
        if (matches)
        {
            Console.WriteLine($"Radio button '{radioFieldName}' options match the expected set.");
        }
        else
        {
            Console.WriteLine($"Radio button '{radioFieldName}' options do NOT match.");
            Console.WriteLine("Expected: " + string.Join(", ", expectedOptions));
            Console.WriteLine("Actual:   " + string.Join(", ", actualOptions));
        }
    }
}
