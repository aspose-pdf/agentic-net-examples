using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";               // Input PDF containing the form
        const string radioFieldName = "Color";           // Fully‑qualified name of the radio group

        // Expected export values for the radio button options
        var expected = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "White",
            "Black",
            "Red"
        };

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF form using the Facades Form class
        Form form = new Form(pdfPath);

        // Retrieve the radio button options: dictionary keyed by item name, value = export value
        Dictionary<string, string> options = form.GetButtonOptionValues(radioFieldName);

        // Collect the actual export values
        var actual = new HashSet<string>(options.Values, StringComparer.OrdinalIgnoreCase);

        // Compare the actual set with the expected set
        if (expected.SetEquals(actual))
        {
            Console.WriteLine("Radio button options match the expected set.");
        }
        else
        {
            Console.WriteLine("Radio button options do NOT match the expected set.");
            Console.WriteLine("Expected values:");
            foreach (var val in expected) Console.WriteLine($"  {val}");
            Console.WriteLine("Actual values:");
            foreach (var val in actual) Console.WriteLine($"  {val}");
        }

        // Clean up the facade
        form.Close();
    }
}