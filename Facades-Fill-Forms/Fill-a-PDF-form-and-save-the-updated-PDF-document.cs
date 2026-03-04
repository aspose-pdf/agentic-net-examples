using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for form handling
using Aspose.Pdf;           // Core PDF types (if needed)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input_form.pdf";
        const string outputPdf = "filled_form.pdf";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Use a Form facade to work with AcroForm fields.
        // The Form class implements IDisposable via SaveableFacade, so wrap it in a using block.
        using (Form form = new Form(inputPdf))
        {
            // Example: fill a text field, a checkbox, a radio button and a list box.
            // Field names must be the fully qualified names as they appear in the PDF.
            // Adjust the names to match your document.

            // Fill a text field
            form.FillField("FirstName", "John");

            // Fill another text field
            form.FillField("LastName", "Doe");

            // Check a checkbox field
            form.FillField("SubscribeNewsletter", true);

            // Select the third option (index = 2) of a radio button group
            form.FillField("Gender", 2); // 0‑based index of the option

            // Select an item in a list box (by index)
            form.FillField("CountryList", 5);

            // Save the updated PDF to a new file.
            // Save(string) writes a PDF regardless of the file extension.
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form filled and saved to '{outputPdf}'.");
    }
}