using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Path to the PDF containing the form
        const string fieldName = "RadioGroup1";      // Fully qualified name of the radio button group

        // Expected options: key = option name, value = export value
        var expectedOptions = new Dictionary<string, string>
        {
            { "OptionA", "ValueA" },
            { "OptionB", "ValueB" },
            { "OptionC", "ValueC" }
        };

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF form using the Facade class (implements IDisposable)
        using (Form form = new Form(pdfPath))
        {
            // Retrieve the option dictionary for the specified radio button field
            Dictionary<string, string> actualOptions = form.GetButtonOptionValues(fieldName);

            if (actualOptions == null || actualOptions.Count == 0)
            {
                Console.WriteLine($"No options found for field '{fieldName}'.");
                return;
            }

            // Compare the actual options with the expected set
            bool allMatch = true;

            // Check for missing or extra options
            if (actualOptions.Count != expectedOptions.Count)
                allMatch = false;

            foreach (var expected in expectedOptions)
            {
                if (!actualOptions.TryGetValue(expected.Key, out string actualValue) ||
                    !string.Equals(actualValue, expected.Value, StringComparison.Ordinal))
                {
                    allMatch = false;
                    Console.WriteLine($"Mismatch for option '{expected.Key}': expected '{expected.Value}', got '{actualValue ?? "null"}'.");
                }
            }

            if (allMatch)
                Console.WriteLine("Radio button options match the expected set.");
            else
                Console.WriteLine("Radio button options do NOT match the expected set.");
        }
    }
}