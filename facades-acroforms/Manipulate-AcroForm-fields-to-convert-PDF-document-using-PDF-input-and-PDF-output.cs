using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for form manipulation

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF with AcroForm
        const string outputPdf = "output.pdf";  // destination PDF after manipulation

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Open the PDF using the Form facade (loads the document internally)
        using (Form form = new Form(inputPdf))
        {
            // Example: list all field names present in the form
            Console.WriteLine("AcroForm fields found:");
            foreach (string name in form.FieldNames)
                Console.WriteLine($" - {name}");

            // Fill a text field (replace with an actual field name from your PDF)
            if (Array.Exists(form.FieldNames, f => f.Equals("FirstName", StringComparison.OrdinalIgnoreCase)))
                form.FillField("FirstName", "John");

            // Fill a checkbox field (true = checked, false = unchecked)
            if (Array.Exists(form.FieldNames, f => f.Equals("Subscribe", StringComparison.OrdinalIgnoreCase)))
                form.FillField("Subscribe", true);

            // Fill a radio button field by index (0‑based index of the option)
            if (Array.Exists(form.FieldNames, f => f.Equals("Gender", StringComparison.OrdinalIgnoreCase)))
                form.FillField("Gender", 1); // selects the second option

            // Fill a list box field with multiple selections
            if (Array.Exists(form.FieldNames, f => f.Equals("Interests", StringComparison.OrdinalIgnoreCase)))
                form.FillField("Interests", new string[] { "Music", "Travel" });

            // Save the modified PDF to the output path
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form processing completed. Output saved to '{outputPdf}'.");
    }
}
