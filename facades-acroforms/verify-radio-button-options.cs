using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormWithRadioButtons.pdf";
        const string radioFieldName = "ColorChoice"; // replace with the actual field name

        // Expected option names (keys) for the radio button group
        var expectedOptions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Red",
            "Green",
            "Blue"
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF form using the Facade API
        Form form = new Form(inputPdf);
        try
        {
            // Retrieve the option values dictionary: key = option name, value = export value
            Dictionary<string, string> optionValues = form.GetButtonOptionValues(radioFieldName);

            // Extract the set of option names present in the document
            var actualOptions = new HashSet<string>(optionValues.Keys, StringComparer.OrdinalIgnoreCase);

            // Compare actual options with expected options
            bool matches = actualOptions.SetEquals(expectedOptions);

            if (matches)
            {
                Console.WriteLine($"Radio button field '{radioFieldName}' contains the expected options.");
            }
            else
            {
                Console.WriteLine($"Radio button field '{radioFieldName}' does NOT match the expected options.");
                Console.WriteLine("Missing options:");
                foreach (var missing in expectedOptions)
                {
                    if (!actualOptions.Contains(missing))
                        Console.WriteLine($"  - {missing}");
                }

                Console.WriteLine("Unexpected options:");
                foreach (var extra in actualOptions)
                {
                    if (!expectedOptions.Contains(extra))
                        Console.WriteLine($"  - {extra}");
                }
            }
        }
        finally
        {
            // Release resources held by the Form facade
            form.Close();
        }
    }
}