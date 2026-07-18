using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";               // Path to the generated PDF containing the form
        const string radioFieldName = "Color";           // Fully qualified name of the radio button group

        // Expected export values for the radio button options
        var expectedOptions = new HashSet<string> { "White", "Black", "Red" };

        // ---------------------------------------------------------------------
        // 1️⃣ Create a minimal PDF with a radio button group named "Color"
        // ---------------------------------------------------------------------
        // The sandbox does not contain any files, so we generate the input PDF
        // program‑matically before we try to read it.
        using (var doc = new Document())
        {
            // Add a single blank page – the field will be placed on this page.
            var page = doc.Pages.Add();

            // Define the rectangle where the radio button group will be rendered.
            // (left, bottom, right, top) – values are in points.
            var rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Create the radio button field. Use the constructor that only takes a Page.
            var radio = new RadioButtonField(page)
            {
                PartialName = radioFieldName,
                // Set the rectangle that defines the field's location on the page.
                Rect = rect
                // Optional: set the default selected option.
                // Value = "White";
            };

            // Add the three options. The first argument is the *option name* (displayed
            // in the UI), the second argument is the *export value* that GetButtonOptionValues
            // returns.
            radio.AddOption("White", "White");
            radio.AddOption("Black", "Black");
            radio.AddOption("Red", "Red");

            // Add the field to the document's form collection.
            doc.Form.Add(radio);

            // Save the PDF so the Facade can open it later.
            doc.Save(pdfPath);
        }

        // ---------------------------------------------------------------------
        // 2️⃣ Open the PDF with the Form facade and verify the radio button options
        // ---------------------------------------------------------------------
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfPath))
        {
            // Retrieve the option dictionary: key = option name, value = export value
            Dictionary<string, string> optionDict = form.GetButtonOptionValues(radioFieldName);

            // Collect the actual export values from the dictionary
            var actualValues = new HashSet<string>(optionDict.Values);

            // Compare the actual set with the expected set
            bool matches = expectedOptions.SetEquals(actualValues);

            Console.WriteLine(matches
                ? "Radio button options match the expected set."
                : "Radio button options differ from the expected set.");

            // If there is a mismatch, report missing and unexpected options
            if (!matches)
            {
                var missing = new HashSet<string>(expectedOptions);
                missing.ExceptWith(actualValues);

                var unexpected = new HashSet<string>(actualValues);
                unexpected.ExceptWith(expectedOptions);

                if (missing.Count > 0)
                    Console.WriteLine("Missing options: " + string.Join(", ", missing));

                if (unexpected.Count > 0)
                    Console.WriteLine("Unexpected options: " + string.Join(", ", unexpected));
            }
        }
    }
}
