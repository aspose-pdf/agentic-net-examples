using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";               // Input PDF containing the form
        const string radioFieldName = "Color";           // Fully qualified name of the radio button group

        // Expected radio button options (key = option name, value = display value)
        var expectedOptions = new Dictionary<string, string>
        {
            { "White", "White" },
            { "Black", "Black" }
        };

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the form using the Facades API
        using (Form form = new Form(pdfPath))
        {
            // Retrieve the actual options for the specified radio button field
            Dictionary<string, string> actualOptions = form.GetButtonOptionValues(radioFieldName);

            // Verify that the actual options match the expected set
            bool isValid = actualOptions.Count == expectedOptions.Count;

            if (isValid)
            {
                foreach (var kvp in expectedOptions)
                {
                    if (!actualOptions.TryGetValue(kvp.Key, out string actualValue) ||
                        actualValue != kvp.Value)
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            Console.WriteLine(isValid
                ? "Radio button options match the expected set."
                : "Radio button options do NOT match the expected set.");
        }
    }
}