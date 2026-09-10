using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";               // Input PDF containing the form
        const string radioFieldName = "Color";           // Fully qualified name of the radio button group

        // Expected radio button options (option name -> export value)
        var expectedOptions = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            { "White", "White" },
            { "Black", "Black" }
        };

        // ------------------------------------------------------------
        // 1. Create a sample PDF with a radio button group named "Color"
        // ------------------------------------------------------------
        CreateSamplePdf(pdfPath);

        // ------------------------------------------------------------
        // 2. Open the PDF with the Form facade and retrieve the options
        // ------------------------------------------------------------
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfPath))
        {
            // Retrieve the actual options for the specified radio button field
            Dictionary<string, string> actualOptions = form.GetButtonOptionValues(radioFieldName);

            // Compare counts first
            if (actualOptions.Count != expectedOptions.Count)
            {
                Console.WriteLine($"Option count mismatch: expected {expectedOptions.Count}, found {actualOptions.Count}.");
            }
            else
            {
                bool allMatch = true;
                foreach (var expected in expectedOptions)
                {
                    if (!actualOptions.TryGetValue(expected.Key, out string actualValue) ||
                        actualValue != expected.Value)
                    {
                        Console.WriteLine($"Mismatch for option '{expected.Key}': expected '{expected.Value}', got '{actualValue ?? "null"}'.");
                        allMatch = false;
                    }
                }

                if (allMatch)
                {
                    Console.WriteLine("Radio button options match the expected set.");
                }
            }
        }
    }

    /// <summary>
    /// Generates a minimal PDF containing a radio button field named "Color"
    /// with two options: "White" and "Black".
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        // Create a new empty PDF document
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Define the rectangle where the radio button group will be placed
        // (left, bottom, right, top) – values are in points.
        var rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

        // Create the radio button field – use the constructor that accepts only a Page
        RadioButtonField radio = new RadioButtonField(page)
        {
            PartialName = "Color",
            Rect = rect
        };

        // Add the two options (display name, export value)
        radio.AddOption("White", "White");
        radio.AddOption("Black", "Black");

        // Add the field to the document's form collection
        doc.Form.Add(radio);

        // Save the PDF so the Form facade can later read it
        doc.Save(path);
    }
}
